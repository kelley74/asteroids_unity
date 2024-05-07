using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Core.Input
{
    public class InputController : MonoBehaviour
    {
        public Action OnFireCast;
        
        [SerializeField] private InputActionReference _forwardAction;
        [SerializeField] private InputActionReference _leftRotationAction;
        [SerializeField] private InputActionReference _rightRotationAction;
        [SerializeField] private InputActionReference _firePressAction;

        public float ForwardSpeed { get; private set; }
        public float RotationSpeed { get; private set; }

        private void Awake()
        {
            Activate(true);
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public void Activate(bool value)
        {
            if (value)
            {
                _forwardAction.action.Enable();
                _leftRotationAction.action.Enable();
                _rightRotationAction.action.Enable();
                _firePressAction.action.Enable();
            }
            else
            {
                _forwardAction.action.Disable();
                _leftRotationAction.action.Disable();
                _rightRotationAction.action.Disable();
                _firePressAction.action.Disable();
            }
        }

        void Update()
        {
            ForwardSpeed = _forwardAction.action.ReadValue<float>();
            RotationSpeed = _rightRotationAction.action.ReadValue<float>() -
                            _leftRotationAction.action.ReadValue<float>();

            if (_firePressAction.action.triggered)
            {
                OnFireCast?.Invoke();
            }
            
        }
    }
}