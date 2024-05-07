using System;
using System.Collections;
using System.Collections.Generic;
using Game.BindingContainer;
using Game.Core.GameSystems;
using Game.Core.Life;
using Game.Core.Movement;
using Game.Core.Colliding;
using Game.Scripts.GameData;
using Game.Scripts.Gameplay.Floaters;
using Game.Services.Pools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Gameplay.Enemies
{
    public class EnemyOwner : MonoBehaviour
    {
        [SerializeField] private EnemySpawnConfig _spawnConfig;

        private readonly Dictionary<ILiveable, EntityConfigurator> _enemies =
            new Dictionary<ILiveable, EntityConfigurator>();

        private bool _levelFinished;
        private IMovable _target;
        private IEnemyLifeFactory _enemyLifeFactory;

        // Round data
        private int _spawnedEnemies;
        private GameRoundData _gameRoundData;

        public void CreateRound(IMovable target, IEnemyLifeFactory enemyLifeFactory, GameRoundData gameRoundData)
        {
            _levelFinished = false;
            _target = target;
            _enemyLifeFactory = enemyLifeFactory;
            _spawnedEnemies = 0;
            _gameRoundData = gameRoundData;
            StartCoroutine(SpawnEnemies());
        }

        public void DestroyRound()
        {
            _levelFinished = true;
            StopAllCoroutines();
            foreach (var enemy in _enemies.Keys)
            {
                var configurator = _enemies[enemy];
                var obj = configurator.GetGameObject();
                var pool = DiContainer.Resolve<GoPool>();
                pool.Push(obj);
                configurator.UnregisterFormAllSystems();
            }

            _enemies.Clear();
        }

        private IEnumerator SpawnEnemies()
        {
            for (int i = 0; i < _spawnConfig.PrewarmCount; i++)
            {
                SpawnEnemy();
            }

            while (!_levelFinished)
            {
                while (_spawnedEnemies >= _spawnConfig.MaxEnemies)
                {
                    yield return null;
                }

                var delay = SpawnEnemy();
                yield return new WaitForSeconds(delay);
            }
        }

        public float SpawnEnemy()
        {
            var enemyConfig = _spawnConfig.GetRandomEnemy();
            // Randomize Position
            var angle = Random.Range(0, 2 * Mathf.PI); // Random angle between 0 and 360
            var position = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0) * enemyConfig.SpawnRadius;
            return SpawnEnemy(enemyConfig, position);
        }

        public float SpawnEnemy(EnemyConfig enemyConfig, Vector3 position)
        {
            // Create Entity Configurator
            var entityConfigurator = new EntityConfigurator();

            // Get Enemy Game Object from Pool
            var pool = DiContainer.Resolve<GoPool>();
            var enemyGameObject = pool.Pull(enemyConfig.Prefab);
            entityConfigurator.AssignGameObject(enemyGameObject);

            // Set position
            enemyGameObject.transform.position = position;

            // Get Movement Component
            var movementComponent = enemyGameObject.GetComponent<IMoveComponent>();

            // Get Movement Entity Controller
            var movementController = enemyConfig.GetMovementController(position, movementComponent);
            var targetHolder = movementController as IMoveTargetHolder;
            targetHolder?.SetTarget(_target);

            // Get Life Entity Controller from Config by Factory
            var lifeController = enemyConfig.GetLifeController(_enemyLifeFactory, movementController);
            if (lifeController == null)
            {
                throw new Exception("Enemy Life controller is not valid");
            }

            var collideController =
                new EnemyCollideController(movementController, (reason) => { lifeController.Release(reason); }, enemyConfig);

            // Register entity controllers in Systems
            entityConfigurator.AddSystem<LifeSystem>(lifeController);
            entityConfigurator.AddSystem<MovementSystem>(movementController);
            entityConfigurator.AddSystem<CollideSystem>(collideController);

            // Add configurator to dictionary using Life Controller as a Key
            _enemies.Add(lifeController, entityConfigurator);

            // Increase number of Enemies
            _spawnedEnemies++;

            // Return Post Spawn Cooldown Delay
            return enemyConfig.PostSpawnDelay;
        }

        public void OnEnemyDestroyed(ILiveable enemy, bool self)
        {
            var configurator = _enemies[enemy];
            _enemies.Remove(enemy);
            var obj = configurator.GetGameObject();
            var pool = DiContainer.Resolve<GoPool>();
            pool.Push(obj);
            configurator.UnregisterFormAllSystems();
            _spawnedEnemies--;
            if (!self)
            {
                _gameRoundData.AddKill();
                DiContainer.Resolve<FloaterSpawner>().SpawnExplosionFloater(obj.transform.position);
            }
        }
    }
}