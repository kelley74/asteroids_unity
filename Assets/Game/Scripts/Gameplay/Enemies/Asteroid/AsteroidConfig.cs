using UnityEngine;

namespace Game.Gameplay.Enemies.Asteroid
{
    [CreateAssetMenu(menuName = "Config/AsteroidConfig")]
    public class AsteroidConfig : EnemyConfig
    {
        [SerializeField] private int _generation;

        public int Generation => _generation;
    }
}