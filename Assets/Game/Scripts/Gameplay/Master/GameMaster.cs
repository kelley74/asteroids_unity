using System;
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
        [SerializeField] private EnemyOwner _enemyOwner;
        [SerializeField] private EnemyDieHandler _enemyDieHandler;
        [SerializeField] private GameplayConfig _gameplayConfig;

        private GameRoundData _gameRoundData;
        private GameplayData _gameplayData;

        private void Awake()
        {
            Application.targetFrameRate = 60;
        }

        public void StartGame()
        {
            _gameRoundData = new GameRoundData();
            _gameplayData = new GameplayData(_gameplayConfig);
            _playerBuilder.CreatePlayer(_gameplayData, FinishGame);
            var lifeFactory = _enemyDieHandler.GetFactory();
            _enemyOwner.CreateRound(_playerBuilder.Player, lifeFactory, _gameRoundData);
            OnGameStarted?.Invoke(_gameplayData);
        }

        private void FinishGame()
        {
            _playerBuilder.DestroyPlayer();
            _enemyOwner.DestroyRound();
            OnGameFinished?.Invoke(_gameRoundData);
        }
    }
}
