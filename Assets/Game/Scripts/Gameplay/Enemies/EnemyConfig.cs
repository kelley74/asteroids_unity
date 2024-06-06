using System;
using Game.Core.Life;
using Game.Core.Movement;
using Game.Gameplay.Enemies.Alien;
using Game.Gameplay.Enemies.Asteroid;
using UnityEngine;

namespace Game.Gameplay.Enemies
{
    [CreateAssetMenu(menuName = "Config/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        public enum EnemyType
        {
            Asteroid,
            AlienShip
        }

        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private float _postSpawnDelay;
        [SerializeField] private float _speed;
        [SerializeField] private float _spawnRadius = 5f;
        [SerializeField] private float _radius = 0.25f;

        public GameObject Prefab => _prefab;
        public float PostSpawnDelay => _postSpawnDelay;
        public float Speed => _speed;
        public float SpawnRadius => _spawnRadius;
        public float Radius => _radius;

        // Kind of abstract factory, so Enemy Owner will not know about IMovable Implementation
        public IMovable GetMovementController(Vector3 position, IMoveEntity moveEntity)
        {
            switch (_enemyType)
            {
                case EnemyType.Asteroid:
                    return new AsteroidMovementController(position, moveEntity, this);
                case EnemyType.AlienShip:
                    return new AlienMovementController(position, moveEntity, this);
            }

            throw new Exception("Movement controller not found");
        }

        public ILiveable GetLifeController(IEnemyLifeFactory factory, IMovable enemyMoveController)
        {
            return factory.GetEnemyLifeController(enemyMoveController, _enemyType, this);
        }
    }
}