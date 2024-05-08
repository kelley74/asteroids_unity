using UnityEngine;

namespace Game.Gameplay.Enemies.Asteroid
{
    [CreateAssetMenu(menuName = "Config/AsteroidConfig")]
    public class AsteroidConfig : EnemyConfig
    {
        [SerializeField] private int _generation;
        [SerializeField] private float _rotationVelocity = 50f;

        public int Generation => _generation;
        public float RotationVelocity => _rotationVelocity;
    }
}