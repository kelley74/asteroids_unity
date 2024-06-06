using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.BindingContainer;
using Game.Core.GameSystems;
using Game.Core.Life;
using Game.Core.Movement;
using Game.Core.Colliding;
using Game.Scripts.GameData;
using Game.Gameplay.Floaters;
using Game.Services.Pools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Gameplay.Enemies
{
    public class EnemySpawnSystem
    {
        private CancellationTokenSource _cancellationToken;

        public void CreateRound(IMovable target, IEnemyLifeFactory enemyLifeFactory, GameRoundData gameRoundData,
            EnemySpawnConfig spawnConfig)
        {
            _cancellationToken = new CancellationTokenSource();
            _target = target;
            _enemyLifeFactory = enemyLifeFactory;
            _spawnedEnemies = 0;
            _gameRoundData = gameRoundData;
            _spawnConfig = spawnConfig;
            Spawn().Forget();
        }

        public void DestroyRound()
        {
            _cancellationToken.Cancel();
            _cancellationToken.Dispose();

            foreach (var enemy in _enemies.Keys)
            {
                var configurator = _enemies[enemy];
                var obj = configurator.GetGameObject();
                var pool = DiContainer.Resolve<GoPool>();
                pool.Push(obj);
                configurator.UnregisterFromAllSystems();
            }

            _enemies.Clear();
        }

        private async UniTask Spawn()
        {
            for (int i = 0; i < _spawnConfig.PrewarmCount; i++)
            {
                SpawnEnemy();
            }

            while (!_cancellationToken.IsCancellationRequested)
            {
                while (_spawnedEnemies >= _spawnConfig.MaxEnemies)
                {
                    await UniTask.Yield();
                }

                var delay = SpawnEnemy();
                var delayMs = (int)(delay * 1000);
                await UniTask.Delay(delayMs);
            }
        }

        public void Update()
        {
            // Nothing to Update yet
        }

        //------govno


        private EnemySpawnConfig _spawnConfig;

        private readonly Dictionary<ILiveable, ComponentConfigurator> _enemies =
            new Dictionary<ILiveable, ComponentConfigurator>();

        private IMovable _target;
        private IEnemyLifeFactory _enemyLifeFactory;

        // Round data
        private int _spawnedEnemies;
        private GameRoundData _gameRoundData;

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
            var entityConfigurator = new ComponentConfigurator();

            // Get Enemy Game Object from Pool
            var pool = DiContainer.Resolve<GoPool>();
            var enemyGameObject = pool.Pull(enemyConfig.Prefab);
            entityConfigurator.AssignGameObject(enemyGameObject);

            // Set position
            enemyGameObject.transform.position = position;

            // Get Movement Component
            var movementComponent = enemyGameObject.GetComponent<IMoveEntity>();

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
                new EnemyCollideController(movementController, (reason) => { lifeController.Release(reason); },
                    enemyConfig);

            // Register entity controllers in Systems

            entityConfigurator.AddSystem<LifeSystem, ILiveable>(lifeController);
            entityConfigurator.AddSystem<MovementSystem, IMovable>(movementController);
            entityConfigurator.AddSystem<CollideSystem, ICollidable>(collideController);

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
            configurator.UnregisterFromAllSystems();
            _spawnedEnemies--;
            if (!self)
            {
                _gameRoundData.AddKill();
                DiContainer.Resolve<FloaterSpawner>().SpawnExplosionFloater(obj.transform.position);
            }
        }
    }
}