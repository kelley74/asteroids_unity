using System;
using Game.BindingContainer;
using Game.Gameplay.Player;
using Game.Gameplay.Enemies;
using Game.Scripts.GameData;
using UnityEngine;

namespace Game.Gameplay.Master
{
    public class GameMaster : MonoBehaviour
    {
        public Action<GameplayData> OnGameStarted;
        public Action<GameRoundData> OnGameFinished;
        
        [SerializeField] private PlayerBuilder _playerBuilder;
        [SerializeField] private EnemyDieHandler _enemyDieHandler;
        [SerializeField] private GameplayConfig _gameplayConfig;
        [SerializeField] private EnemySpawnConfig _spawnConfig;

        private GameRoundData _gameRoundData;
        private GameplayData _gameplayData;
        private GameLoop _gameLoop;

        private void Awake()
        {
            Application.targetFrameRate = 60;
            _gameLoop = new GameLoop();
            _gameLoop.StartLoop();
        }

        private void OnDestroy()
        {
            _gameLoop.StopLoop();
        }

        public void StartGame()
        {
            _gameRoundData = new GameRoundData();
            _gameplayData = new GameplayData(_gameplayConfig);
            _playerBuilder.CreatePlayer(_gameplayData, FinishGame);
            var lifeFactory = _enemyDieHandler.GetFactory();
            var enemySpawner = DiContainer.Resolve<EnemySpawnSystem>();
            enemySpawner.CreateRound(_playerBuilder.Player, lifeFactory, _gameRoundData, _spawnConfig);
            OnGameStarted?.Invoke(_gameplayData);
        }

        private void FinishGame()
        {
            _playerBuilder.DestroyPlayer();
            var enemySpawner = DiContainer.Resolve<EnemySpawnSystem>();
            enemySpawner.DestroyRound();
            OnGameFinished?.Invoke(_gameRoundData);
        }
    }
}
