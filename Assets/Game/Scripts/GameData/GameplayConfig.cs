using UnityEngine;

namespace Game.Scripts.GameData
{
    [CreateAssetMenu(menuName = "Config/Gameplay")]
    public class GameplayConfig : ScriptableObject
    {
        [SerializeField] private float _laserCooldown = 1f;
        [SerializeField] private int _laserAmmo = 5;

        public float LaserCooldown => _laserCooldown;
        public int LaserAmmo => _laserAmmo;
    }
}