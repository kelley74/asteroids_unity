using System.Collections.Generic;

namespace Game.Core.GameSystems
{
    /// <summary>
    /// Abstract Game System. All systems should be inherited from Game System
    /// </summary>
    public abstract class GameSystem
    {
        // ReSharper disable once InconsistentNaming
        protected readonly List<IGameComponent> _componentsList = new List<IGameComponent>();

        public void AddEntity<T>(T component) where T : IGameComponent
        {
            _componentsList.Add(component);
        }

        public void RemoveEntity<T>(T component) where T : IGameComponent
        {
            _componentsList.Remove(component);
        }
        public abstract void Update();
    }

    public interface IGameComponent
    {
    }
}