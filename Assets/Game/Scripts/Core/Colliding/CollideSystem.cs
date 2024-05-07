using Game.Core.GameSystems;

namespace Game.Core.Colliding
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
                    
                    // ReSharper disable once IdentifierTypo
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
}
