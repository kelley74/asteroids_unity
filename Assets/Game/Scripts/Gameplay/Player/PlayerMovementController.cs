using System;
using Game.BindingContainer;
using Game.Core.Input;
using Game.Core.Movement;
using UnityEngine;

namespace Game.Gameplay.Player
{
    public class BulletMovementController : IMovable
    {
        private Vector3 _position;
        private float _angle;
        private readonly IMoveComponent _moveComponent;
        private float _velocity;
        
        public BulletMovementController(Vector3 initialPosition, float angle, IMoveComponent component)
        {
            _position = initialPosition;
            _angle = angle;
            _moveComponent = component;
        }

        public void Move(float deltaTime)
        {
            var radAngle = _angle * Mathf.PI / 180;
            var direction = new Vector3(-Mathf.Sin(radAngle), Mathf.Cos(radAngle), 0).normalized;
            _velocity = deltaTime * 10; // Add config
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
    
    public class PlayerMovementController : IMovable
    {
        private Vector3 _directionalVelocity;
        private Vector3 _position;
        private float _angle;
        private float _angleTarget;
        private float _rawVelocity;
        private float _velocity;

        private readonly IMoveComponent _moveComponent;
        private readonly InputController _inputController;
        private readonly PlayerConfig _config;
        private readonly Vector3 _boundaries;

        public PlayerMovementController(Vector3 initialPosition, IMoveComponent component, PlayerConfig config)
        {
            _position = initialPosition;
            _moveComponent = component;
            _config = config;
            _inputController = DiContainer.Resolve<InputController>();
            var camera = Camera.main;
            var ratio = (float)Screen.width / Screen.height;
            if (camera == null)
            {
                throw new Exception("Camera not found");
            }

            var size = camera.orthographicSize;
            _boundaries = new Vector3(ratio * size, size) * _config.BoundariesScale;
        }

        IMoveComponent IMovable.GetMoveComponent()
        {
            return _moveComponent;
        }

        public float GetVelocity()
        {
            return _velocity;
        }

        Vector3 IMovable.GetPosition()
        {
            return _position;
        }

        float IMovable.GetDirectionAngle()
        {
            return _angle;
        }

        void IMovable.Move(float deltaTime)
        {
            _rawVelocity = Mathf.Lerp(_rawVelocity, _inputController.ForwardSpeed,
                deltaTime *
                (_inputController.ForwardSpeed > Mathf.Epsilon ? _config.Acceleration : _config.Deceleration));

            // Rotation Calculations
            // Angle Target is an Angle that will be calculated by Input
            // Angle is being calculated taking the Angular Acceleration into account
            _angleTarget += -_inputController.RotationSpeed * deltaTime * _config.AngularSpeed;
            _angle = Mathf.Lerp(_angle, _angleTarget, deltaTime * _config.AngularAcceleration);

            var angleRad = Mathf.PI * _angle / 180f;
            var direction = new Vector3(-Mathf.Sin(angleRad), Mathf.Cos(angleRad), 0);
            _directionalVelocity = Vector3.Lerp(_directionalVelocity, direction,
                deltaTime * (_inputController.ForwardSpeed > Mathf.Epsilon
                    ? _config.DirectionalActiveInterpolation
                    : _config.DirectionalInactiveInterpolation));

            _velocity = deltaTime * _rawVelocity * _config.SpeedScale;
            _position += _directionalVelocity.normalized * _velocity;

            // Teleport Player across the view it's out of camera boundaries
            if (_position.x > _boundaries.x)
            {
                _position.x -= _boundaries.x * _config.TeleportOffset;
            }

            if (_position.x < -_boundaries.x)
            {
                _position.x += _boundaries.x * _config.TeleportOffset;
            }

            if (_position.y > _boundaries.y)
            {
                _position.y -= _boundaries.y * _config.TeleportOffset;
            }

            if (_position.y < -_boundaries.y)
            {
                _position.y += _boundaries.y * _config.TeleportOffset;
            }
        }
    }
}