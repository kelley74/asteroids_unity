using Game.Core.Movement;
using UnityEngine;

namespace Game.Gameplay.Player
{
    public class BulletMovementController : IMovable
    {
        private readonly IMoveEntity _moveEntity;
        private readonly float _speed;
        private readonly float _angle;
        
        private Vector3 _position;
        private float _velocity;
        
        public BulletMovementController(Vector3 initialPosition, float angle, IMoveEntity entity, float speed)
        {
            _position = initialPosition;
            _angle = angle;
            _speed = speed;
            _moveEntity = entity;
        }

        public void Move(float deltaTime)
        {
            var radAngle = _angle * Mathf.PI / 180;
            var direction = new Vector3(-Mathf.Sin(radAngle), Mathf.Cos(radAngle), 0).normalized;
            _velocity = deltaTime * _speed;
            _position += direction * _velocity;
            var rotation = Quaternion.Euler(Vector3.forward * _angle);
            _moveEntity.SetPositionAndRotation(_position, rotation);
        }

        public Vector3 GetPosition()
        {
            return _position;
        }

        public float GetDirectionAngle()
        {
            return _angle;
        }

        public IMoveEntity GetMoveEntity()
        {
            return _moveEntity;
        }

        public float GetVelocity()
        {
            return _velocity;
        }
    }
}