using Game.Core.Movement;
using UnityEngine;

namespace Game.Gameplay.Player
{
    public class BulletMovementController : IMovable
    {
        private readonly IMoveComponent _moveComponent;
        private readonly float _speed;
        private readonly float _angle;
        
        private Vector3 _position;
        private float _velocity;
        
        public BulletMovementController(Vector3 initialPosition, float angle, IMoveComponent component, float speed)
        {
            _position = initialPosition;
            _angle = angle;
            _speed = speed;
            _moveComponent = component;
        }

        public void Move(float deltaTime)
        {
            var radAngle = _angle * Mathf.PI / 180;
            var direction = new Vector3(-Mathf.Sin(radAngle), Mathf.Cos(radAngle), 0).normalized;
            _velocity = deltaTime * _speed;
            _position += direction * _velocity;
        }

        public Vector3 GetPosition()
        {
            return _position;
        }

        public float GetDirectionAngle()
        {
            return _angle;
        }

        public IMoveComponent GetMoveComponent()
        {
            return _moveComponent;
        }

        public float GetVelocity()
        {
            return _velocity;
        }
    }
}