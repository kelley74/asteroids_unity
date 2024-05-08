using Game.Core.Movement;
using UnityEngine;

namespace Game.Gameplay.Enemies.Alien
{
    public class AlienMovementController : IMovable, IMoveTargetHolder
    {
        private readonly IMoveComponent _moveComponent;
        private readonly AlienConfig _config;
        
        private Vector3 _position;
        private float _rotationAngle;
        private IMovable _target;
        private Vector3 _currentDirection;
        private float _velocity;

        public AlienMovementController(Vector3 initialPosition, IMoveComponent component, EnemyConfig config)
        {
            _position = initialPosition;
            _moveComponent = component;
            _config = (AlienConfig)config;
            _rotationAngle = 0;
        }

        void IMovable.Move(float deltaTime)
        {
            var direction = (_target.GetPosition() - _position).normalized;
            _currentDirection = Vector3.Lerp(_currentDirection, direction, deltaTime * _config.AngularVelocity)
                .normalized;
            _rotationAngle = Vector2.SignedAngle(Vector2.up, new Vector2(_currentDirection.x, _currentDirection.y));
            _velocity = _config.Speed * deltaTime;
            _position += _currentDirection * _velocity;
        }

        Vector3 IMovable.GetPosition()
        {
            return _position;
        }

        float IMovable.GetDirectionAngle()
        {
            return _rotationAngle;
        }

        IMoveComponent IMovable.GetMoveComponent()
        {
            return _moveComponent;
        }

        public float GetVelocity()
        {
            return _velocity;
        }

        void IMoveTargetHolder.SetTarget(IMovable target)
        {
            _target = target;
        }
    }
}