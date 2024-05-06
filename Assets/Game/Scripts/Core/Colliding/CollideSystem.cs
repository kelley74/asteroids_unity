using Game.Core.GameSystems;
using UnityEngine;

namespace Game.Scripts.Core.Colliding
{
    public class CollideSystem : GameSystem
    {
        private void Update()
        {
            
            
            
            for (int i = 0; i < _entityList.Count; i++)
            {
                for (int j = 0; j < _entityList.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    if (_entityList[i] is not ICollidable colliderEntity)
                    {
                        continue;
                    }
                    
                    if (_entityList[j] is not ICollidable collidable)
                    {
                        continue;
                    }

                    if (colliderEntity.CheckCollisions(collidable))
                    {
                        collidable.CheckCollisions(colliderEntity);
                        i = 0;
                        j = 0;
                    }
                }
            }
        }
    }

    public interface ICollidable : IGameEntity
    {
        public enum ColliderType
        {
            Player,
            Bullet,
            Laser,
            Enemy
        }
        bool CheckCollisions(ICollidable collidable);

        ColliderType GetColliderType();

        Vector3 GetPosition();

        float GetRadius();
    }
}
