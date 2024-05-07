using System.Collections.Generic;
using Game.BindingContainer;
using UnityEngine;

namespace Game.Core.GameSystems
{
    /// <summary>
    /// Presents an Entity that can have Configuration of Systems and reference to a Game Object
    /// </summary>
    public class EntityConfigurator
    {
        private readonly Dictionary<GameSystem, IGameEntity> _configuration = new Dictionary<GameSystem, IGameEntity>();
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