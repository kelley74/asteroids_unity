using System;
using Game.BindingContainer;
using Game.Core.GameSystems;
using Game.Core.Input;
using Game.Core.Movement;
using Game.Core.Colliding;
using Game.Scripts.GameData;
using Game.Gameplay.Floaters;
using Game.Services.Pools;
using UnityEngine;

namespace Game.Gameplay.Player
{
    public class PlayerBuilder : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private GameObject _laserPrefab;
        [SerializeField] private PlayerConfig _config;

        public IMovable Player => _playerMovementController;
        
        private EntityConfigurator _playerConfigurator;
        private IMovable _playerMovementController;

        private bool _isGameActive;
        private GameplayData _gameplayData;
        private PlayerShootController _playerShootController;

        public void CreatePlayer(GameplayData gameplayData, Action onRoundComplete)
        {
            _gameplayData = gameplayData;
            _playerShootController = new PlayerShootController();
            var pool = DiContainer.Resolve<GoPool>();
            var playerObject = pool.Pull(_prefab);
            // Movement Controller
            var movementComponent = playerObject.GetComponent<IMoveComponent>();
            _playerMovementController = new PlayerMovementController(Vector3.zero, movementComponent, _config);
            var collideController = new PlayerCollideController(_playerMovementController, onRoundComplete);

            _playerConfigurator = new EntityConfigurator();
            _playerConfigurator.AssignGameObject(playerObject);

            _playerConfigurator.AddSystem<MovementSystem>(_playerMovementController);
            _playerConfigurator.AddSystem<CollideSystem>(collideController);

            // Start Constant Shooting
            StartCoroutine(_playerShootController.ShootBullets(_playerMovementController, playerObject.transform,
                _bulletPrefab, _config));
            _isGameActive = true;

            var input = DiContainer.Resolve<InputController>();
            input.OnFireCast += HandleFire;
        }

        public void DestroyPlayer()
        {
            DiContainer.Resolve<FloaterSpawner>().SpawnExplosionFloater(_playerMovementController.GetPosition());
            StopAllCoroutines();

            var pool = DiContainer.Resolve<GoPool>();
            pool.Push(_playerConfigurator.GetGameObject());

            _playerConfigurator.UnregisterFormAllSystems();

            _playerMovementController = null;
            _isGameActive = false;

            var input = DiContainer.Resolve<InputController>();
            input.OnFireCast -= HandleFire;
        }

        private void Update()
        {
            if (!_isGameActive)
            {
                return;
            }

            var angle = _playerMovementController.GetDirectionAngle();
            _gameplayData.SetPlayerState(_playerMovementController.GetVelocity(), angle,
                _playerMovementController.GetPosition());
            _gameplayData.UpdateState(Time.time);
        }

        private void HandleFire()
        {
            if (_gameplayData.LaserShoot())
            {
                _playerShootController.ShootLaser(_playerMovementController,
                    _playerConfigurator.GetGameObject().transform, _laserPrefab, _config);
            }
        }
    }
}