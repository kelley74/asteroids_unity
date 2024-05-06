using Game.BindingContainer;
using Game.Core.Life;
using UnityEngine;

namespace Game.Gameplay.Enemies
{
    public class EnemyDieHandler : MonoBehaviour
    {
        [SerializeField] private EnemyOwner _enemyOwner;
        [SerializeField] private EnemyConfig _asteroidLevel1;
        [SerializeField] private EnemyConfig _asteroidLevel2;

        public IEnemyLifeFactory GetFactory()
        {
            return new EnemiesLifeFactory(OnAlienDied, OnAsteroidDied);
        }

        private void OnAlienDied(ILiveable liveable)
        {
            _enemyOwner.OnEnemyDestroyed(liveable, false);
        }

        private void OnAsteroidDied(ILiveable liveable, bool selfDestroyed, int generation, Vector3 position)
        {
            if (!selfDestroyed && generation == 0 || generation == 1)
            {
                var config = generation == 0 ? _asteroidLevel1 : _asteroidLevel2;
                _enemyOwner.SpawnEnemy(config,
                    position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0));
                _enemyOwner.SpawnEnemy(config,
                    position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0));
            }

            _enemyOwner.OnEnemyDestroyed(liveable, selfDestroyed);
        }
    }
}