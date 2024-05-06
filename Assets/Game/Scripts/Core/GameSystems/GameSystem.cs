using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.GameSystems
{
    public abstract class GameSystem : MonoBehaviour
    {
        protected List<IGameEntity> _entityList = new List<IGameEntity>();
        
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