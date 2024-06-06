using System;
using Game.BindingContainer;
using Game.Core.Life;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Gameplay.Enemies
{
    public class EnemyDieHandler : MonoBehaviour
    {
        [SerializeField] private EnemyConfig _asteroidLevel1;
        [SerializeField] private EnemyConfig _asteroidLevel2;
        
        private EnemySpawnSystem _enemySpawnSystem;

        private void Start()
        {
            _enemySpawnSystem = DiContainer.Resolve<EnemySpawnSystem>();
        }

        public IEnemyLifeFactory GetFactory()
        {
            return new EnemiesLifeFactory(OnAlienDied, OnAsteroidDied);
        }

        private void OnAlienDied(ILiveable liveable)
        {
            _enemySpawnSystem.OnEnemyDestroyed(liveable, false);
        }

        private void OnAsteroidDied(ILiveable liveable, DeadReason reason, int generation, Vector3 position)
        {
            if (reason != DeadReason.Self && generation == 0 || generation == 1)
            {
                if (reason == DeadReason.Bullet)
                {
                    var config = generation == 0 ? _asteroidLevel1 : _asteroidLevel2;
                    _enemySpawnSystem.SpawnEnemy(config,
                        position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0));
                    _enemySpawnSystem.SpawnEnemy(config,
                        position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0));
                }
                
            }

            _enemySpawnSystem.OnEnemyDestroyed(liveable, reason == DeadReason.Self);
        }
    }
}