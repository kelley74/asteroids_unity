using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.GameSystems
{
    /// <summary>
    /// Abstract Game System. All systems should be inherited from Game System
    /// </summary>
    public abstract class GameSystem : MonoBehaviour
    {
        // ReSharper disable once InconsistentNaming
        protected readonly List<IGameEntity> _entityList = new List<IGameEntity>();
        
        public void AddEntity(IGameEntity entity)
        {
            _entityList.Add(entity);
        }

        public void RemoveEntity(IGameEntity entity)
        {
            _entityList.Remove(entity);
        }
    }

    public interface IGameEntity
    {
        
    }
}