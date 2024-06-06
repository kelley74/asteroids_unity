using System.Collections;
using Game.BindingContainer;
using Game.Core.Colliding;
using Game.Core.GameSystems;
using Game.Core.Life;
using Game.Core.Movement;
using Game.Gameplay.Floaters;
using Game.Services.Pools;
using UnityEngine;

namespace Game.Gameplay.Player
{
    public class PlayerShootController
    {
        public IEnumerator ShootBullets(IMovable playerMoveController, Transform playerGameObjectTransform,
            GameObject projectilePrefab, PlayerConfig config)
        {
            var pool = DiContainer.Resolve<GoPool>();

            while (true)
            {
                var configurator = new ComponentConfigurator();
                var bullet = pool.Pull(projectilePrefab);
                configurator.AssignGameObject(bullet);
                var movementComponent = bullet.GetComponent<IMoveEntity>();

                var position = playerMoveController.GetPosition();
                
                var moveController = new BulletMovementController(position,
                    playerGameObjectTransform.eulerAngles.z, movementComponent, config.BulletSpeed);

                var lifeModel = new BulletLifeModel((_) =>
                {
                    configurator.UnregisterFromAllSystems();
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
                    configurator.UnregisterFromAllSystems();
                    pool.Push(bulletObject);
                });

                configurator.AddSystem<MovementSystem, IMovable>(moveController);
                configurator.AddSystem<LifeSystem, ILiveable>(lifeController);
                configurator.AddSystem<CollideSystem, ICollidable>(collideController);

                yield return new WaitForSeconds(config.BulletCooldownTime);
            }
        }

        public void ShootLaser(IMovable playerMoveController, Transform playerGameObjectTransform,
            GameObject projectilePrefab, PlayerConfig config)
        {
            var pool = DiContainer.Resolve<GoPool>();

            var configurator = new ComponentConfigurator();
            var laser = pool.Pull(projectilePrefab);
            configurator.AssignGameObject(laser);
            var movementComponent = laser.GetComponent<IMoveEntity>();

            var moveController = new BulletMovementController(playerMoveController.GetPosition(),
                playerGameObjectTransform.eulerAngles.z, movementComponent, config.LaserSpeed);

            var lifeModel = new BulletLifeModel((_) =>
            {
                configurator.UnregisterFromAllSystems();
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
            });

            configurator.AddSystem<MovementSystem, IMovable>(moveController);
            configurator.AddSystem<LifeSystem, ILiveable>(lifeController);
            configurator.AddSystem<CollideSystem, ICollidable>(collideController);
        }
    }
}