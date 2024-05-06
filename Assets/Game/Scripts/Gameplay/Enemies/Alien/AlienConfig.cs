using UnityEngine;

namespace Game.Gameplay.Enemies.Alien
{
    [CreateAssetMenu(menuName = "Config/AlienConfig")]
    public class AlienConfig : EnemyConfig
    {
        [SerializeField] private float _angularVelocity = 5f;

        public float AngularVelocity => _angularVelocity;
    }
}
