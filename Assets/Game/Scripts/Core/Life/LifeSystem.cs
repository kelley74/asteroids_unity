using System.Collections.Generic;
using Game.Core.GameSystems;

namespace Game.Core.Life
{
    public class LifeSystem : GameSystem
    {
        public override void Update()
        {
            var deadEntities = new List<ILiveable>();
            foreach (var entity in _componentsList)
            {
                if (entity is not ILiveable lifeEntity)
                {
                    continue;
                }

                if (lifeEntity.IsAlive())
                {
                    lifeEntity.CheckLive();
                    if (!lifeEntity.IsAlive())
                    {
                        deadEntities.Add(lifeEntity);
                    }
                }
            }

            foreach (var deadEntity in deadEntities)
            {
                deadEntity.Release(DeadReason.Self);
            }
        }
    }
}