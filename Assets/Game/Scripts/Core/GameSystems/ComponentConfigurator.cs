using System.Collections.Generic;
using Game.BindingContainer;
using UnityEngine;

namespace Game.Core.GameSystems
{
    /// <summary>
    /// Presents an Entity that can have Configuration of Systems and reference to a Game Object
    /// </summary>
    public class ComponentConfigurator
    {
        private readonly Dictionary<GameSystem, IGameComponent> _configuration =
            new Dictionary<GameSystem, IGameComponent>();

        private GameObject _gameObject;

        public void AddSystem<TSystem,TComponent>(TComponent component) where TSystem : GameSystem
        where TComponent : IGameComponent
        {
            var system = DiContainer.Resolve<TSystem>();
            system.AddEntity(component);
            _configuration.Add(system, component);
        }

        public void AssignGameObject(GameObject gameObject)
        {
            _gameObject = gameObject;
        }

        public GameObject GetGameObject() => _gameObject;

        public void UnregisterFromAllSystems()
        {
            foreach (var system in _configuration.Keys)
            {
                system.RemoveEntity(_configuration[system]);
            }
        }
    }
}