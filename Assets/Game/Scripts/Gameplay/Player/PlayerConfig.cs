using UnityEngine;

namespace Game.Gameplay.Player
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Config/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [Header("Movement")]
        [SerializeField] private float _acceleration = 3f;
        [SerializeField] private float _deceleration = 1f;
        [SerializeField] private float _dirActiveInterpolation = 3f;
        [SerializeField] private float _dirInactiveInterpolation = 1f;
        [SerializeField] private float _speedScale = 5f;
        [SerializeField] private float _angularSpeed = 100f;
        [SerializeField] private float _angularAcceleration = 5f;
        [SerializeField] private float _boundariesScale = 1.1f;
        [SerializeField] private float _teleportOffset = 1.9f;
        
        [Header("Shooting")]
        [SerializeField] private float _bulletCooldown = 0.5f;

        public float Acceleration => _acceleration;
        public float Deceleration => _deceleration;
        public float DirectionalActiveInterpolation => _dirActiveInterpolation;
        public float DirectionalInactiveInterpolation => _dirInactiveInterpolation;
        public float AngularSpeed => _angularSpeed;
        public float AngularAcceleration => _angularAcceleration;
        public float SpeedScale => _speedScale;
        public float BoundariesScale => _boundariesScale;
        public float TeleportOffset => _teleportOffset;
        public float BulletCooldownTime => _bulletCooldown;
    }
}
