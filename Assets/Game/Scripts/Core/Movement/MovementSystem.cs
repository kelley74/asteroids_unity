using Game.Core.GameSystems;
using UnityEngine;

namespace Game.Core.Movement
{
    public class MovementSystem : GameSystem
    {
        private void Update()
        {
            foreach (var e in _entityList)
            {
                if (e is not IMovable entity)
                {
                    continue;
                }
                
                // Calculate position and rotation in every entity movement controller
                entity.Move(Time.deltaTime);
                var position = entity.GetPosition();
                var rotation = Quaternion.Euler(Vector3.forward * entity.GetDirectionAngle());
                
                // Set position and rotation to a visual representation
                var moveComponent = entity.GetMoveComponent();
                moveComponent.SetPositionAndRotation(position, rotation);
            }
        }
    }
}