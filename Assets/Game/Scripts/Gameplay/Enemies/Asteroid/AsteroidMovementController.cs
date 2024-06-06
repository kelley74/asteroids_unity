using Game.Core.Movement;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Gameplay.Enemies.Asteroid
{
    public class AsteroidMovementController : IMovable
    {
        private readonly Vector3 _direction;
        private readonly AsteroidConfig _config;
        private readonly float _angularVelocity;
        
        private Vector3 _position;
        private readonly IMoveEntity _moveEntity;
        private float _rotationAngle;
        private float _velocity;
        
        public AsteroidMovementController(Vector3 initialPosition, IMoveEntity entity, EnemyConfig config)
        {
            _position = initialPosition;
            _moveEntity = entity;
            _config = (AsteroidConfig)config;
            
            var angle = Random.Range(0, 2 * Mathf.PI);
            _rotationAngle = angle * 180 / Mathf.PI;
            _direction = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0).normalized;
            _angularVelocity = Random.Range(-_config.RotationVelocity, _config.RotationVelocity);
        }

        void IMovable.Move(float deltaTime)
        {
            _velocity = _config.Speed * deltaTime;
            _position += _direction * _velocity;
            _rotationAngle += _angularVelocity * deltaTime;
            var rotation = Quaternion.Euler(Vector3.forward * _rotationAngle);
            _moveEntity.SetPositionAndRotation(_position, rotation);
        }

        Vector3 IMovable.GetPosition()
        {
            return _position;
        }

        float IMovable.GetDirectionAngle()
        {
            return _rotationAngle;
        }

        public float GetVelocity()
        {
            return _velocity;
        }
    }
}