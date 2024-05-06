using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Gameplay.Enemies
{
    [CreateAssetMenu(menuName = "Config/EnemySpawnConfig")]
    public class EnemySpawnConfig : ScriptableObject
    {
        [Serializable]
        private class EnemyConfigByChance
        {
            [SerializeField] private EnemyConfig _config;
            [SerializeField] [Range(0f,1f)] private float _chance;

            public EnemyConfig Config => _config;
            public float Chance => _chance;
        }

        [SerializeField] private EnemyConfigByChance[] _configs;
        // ReSharper disable once IdentifierTypo
        [SerializeField] private int _prewarmCount = 10;
        [SerializeField] private int _maxEnemies = 100;
        
        // ReSharper disable once IdentifierTypo
        public int PrewarmCount => _prewarmCount;
        public int MaxEnemies => _maxEnemies;

        public EnemyConfig GetRandomEnemy()
        {
            float max = 0f;
            int index = -1;
            for (int i = 0; i < _configs.Length; i++)
            {
                var value = Random.Range(0, _configs[i].Chance);
                if (value > max)
                {
                    max = value;
                    index = i;
                }
            }

            if (index < 0)
            {
                throw new Exception("Config not found");
            }

            return _configs[index].Config;
        }
    }
}