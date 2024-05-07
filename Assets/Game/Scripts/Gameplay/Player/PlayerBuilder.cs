using System;
using System.Collections;
using Game.BindingContainer;
using Game.Core.GameSystems;
using Game.Core.Input;
using Game.Core.Life;
using Game.Core.Movement;
using Game.Core.Colliding;
using Game.Scripts.GameData;
using Game.Scripts.Gameplay.Floaters;
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

        // Systems
        private EntityConfigurator _playerConfigurator;

        // Controllers
        private IMovable _playerMovementController;
        public IMovable Player => _playerMovementController;

        private bool _isGameActive;
        private GameplayData _gameplayData;

        public void CreatePlayer(GameplayData gameplayData, Action onRoundComplete)
        {
            _gameplayData = gameplayData;
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

            // TODO Create special class
            StartCoroutine(ShootBullets(_playerMovementController, playerObject.transform));
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
                ShootLaser(_playerMovementController, _playerConfigurator.GetGameObject().transform);
            }
        }

        private IEnumerator ShootBullets(IMovable playerMoveController, Transform playerGameObjectTransform)
        {
            var pool = DiContainer.Resolve<GoPool>();

            while (true)
            {
                var configurator = new EntityConfigurator();
                var bullet = pool.Pull(_bulletPrefab);
                configurator.AssignGameObject(bullet);
                var movementComponent = bullet.GetComponent<IMoveComponent>();

                var moveController = new BulletMovementController(playerMoveController.GetPosition(),
                    playerGameObjectTransform.eulerAngles.z, movementComponent, _config.BulletSpeed);

                var lifeModel = new BulletLifeModel((t) =>
                {
                    configurator.UnregisterFormAllSystems();
                    var obj = configurator.GetGameObject();
                    pool.Push(obj);
                }, moveController);
                var lifeController = new BulletLifeController();
                lifeController.Init(lifeModel);

                var collideController = new BulletCollideController(moveController, () =>
                {
                    var bulletObject = configurator.GetGameObject();
                    var floaterSpawner = DiContainer.Resolve<FloaterSpawner>();
                    floaterSpawner.SpawnBulletFloater(bulletObject.transform.position);
                    configurator.UnregisterFormAllSystems();
                    pool.Push(bulletObject);
                });

                configurator.AddSystem<MovementSystem>(moveController);
                configurator.AddSystem<LifeSystem>(lifeController);
                configurator.AddSystem<CollideSystem>(collideController);

                yield return new WaitForSeconds(_config.BulletCooldownTime);
            }
        }
        
        private void ShootLaser(IMovable playerMoveController, Transform playerGameObjectTransform)
        {
            var pool = DiContainer.Resolve<GoPool>();

            var configurator = new EntityConfigurator();
            var laser = pool.Pull(_laserPrefab);
            configurator.AssignGameObject(laser);
            var movementComponent = laser.GetComponent<IMoveComponent>();

            var moveController = new BulletMovementController(playerMoveController.GetPosition(),
                playerGameObjectTransform.eulerAngles.z, movementComponent, _config.LaserSpeed);

            var lifeModel = new BulletLifeModel((t) =>
            {
                configurator.UnregisterFormAllSystems();
                var obj = configurator.GetGameObject();
                pool.Push(obj);
            }, moveController);
            var lifeController = new BulletLifeController();
            lifeController.Init(lifeModel);

            var collideController = new LaserCollideController(moveController, () =>
            {
                var bulletObject = configurator.GetGameObject();
                var floaterSpawner = DiContainer.Resolve<FloaterSpawner>();
                floaterSpawner.SpawnBulletFloater(bulletObject.transform.position);
                //configurator.UnregisterFormAllSystems();
                //pool.Push(bulletObject);
            });

            configurator.AddSystem<MovementSystem>(moveController);
            configurator.AddSystem<LifeSystem>(lifeController);
            configurator.AddSystem<CollideSystem>(collideController);
        }
    }
}