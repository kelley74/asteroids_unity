using System;
using Game.Core.Life;
using Game.Core.Movement;
using Game.Gameplay.Enemies.Alien;
using Game.Gameplay.Enemies.Asteroid;
using UnityEngine;

namespace Game.Gameplay.Enemies
{
    public class EnemiesLifeFactory : IEnemyLifeFactory
    {
        private Action<ILiveable,DeadReason,int,Vector3> _onAsteroidDied;
        private Action<ILiveable> _onAlienShipDied;

        public EnemiesLifeFactory(Action<ILiveable> onAlienShipDied, Action<ILiveable,DeadReason,int,Vector3> onAsteroidDied)
        {
            _onAlienShipDied = onAlienShipDied;
            _onAsteroidDied = onAsteroidDied;
        }

        public ILiveable GetEnemyLifeController(IMovable enemyMoveController, EnemyConfig.EnemyType type,
            EnemyConfig config)
        {
            ILiveable lifeController = null;
            switch (type)
            {
                case EnemyConfig.EnemyType.Asteroid:
                    lifeController = new AsteroidLifeController();
                    var asteroidConfig = config as AsteroidConfig;
                    if (asteroidConfig == null)
                    {
                        throw new Exception("Asteroid config is not valid");
                    }

                    var asteroidLifeModel = new AsteroidLifeModel(enemyMoveController, _onAsteroidDied,
                        asteroidConfig.Generation);
                    lifeController.Init(asteroidLifeModel);
                    break;
                case EnemyConfig.EnemyType.AlienShip:
                    lifeController = new AlienLifeController();
                    var alienConfig = config; // No specific config for Alien
                    if (alienConfig == null)
                    {
                        throw new Exception("Asteroid config is not valid");
                    }

                    var alienLifeModel = new AlienLifeModel(_onAlienShipDied);
                    lifeController.Init(alienLifeModel);
                    break;
            }
            
            return lifeController;
        }
    }
}