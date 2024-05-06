using System.Collections.Generic;
using Game.BindingContainer;
using UnityEngine;

namespace Game.Core.GameSystems
{
    public class EntityConfigurator
    {
        private Dictionary<GameSystem, IGameEntity> _configuration = new Dictionary<GameSystem, IGameEntity>();
        private GameObject _gameObject;

        public void AddSystem<T>(IGameEntity entity) where T : GameSystem
        {
            var system = DiContainer.Resolve<T>();
            system.AddEntity(entity);
            _configuration.Add(system, entity);
        }

        public void AssignGameObject(GameObject gameObject)
        {
            _gameObject = gameObject;
        }

        public GameObject GetGameObject() => _gameObject;

        public void UnregisterFormAllSystems()
        {
            foreach (var system in _configuration.Keys)
            {
                system.RemoveEntity(_configuration[system]);
            }
        }
    }
}