using Game.Core.GameSystems;

namespace Game.Core.Colliding
{
    public class CollideSystem : GameSystem
    {
        public override void Update()
        {
            
            for (int i = 0; i < _componentsList.Count; i++)
            {
                for (int j = 0; j < _componentsList.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    if (_componentsList[i] is not ICollidable colliderEntity)
                    {
                        continue;
                    }
                    
                    // ReSharper disable once IdentifierTypo
                    if (_componentsList[j] is not ICollidable collidable)
                    {
                        continue;
                    }

                    // TODO: Make removing-safe system
                    // We start bypassing the entity list as a fallback 
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
