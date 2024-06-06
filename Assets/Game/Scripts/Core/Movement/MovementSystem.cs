using Game.Core.GameSystems;
using UnityEngine;

namespace Game.Core.Movement
{
    public class MovementSystem : GameSystem
    {
        public override void Update()
        {
            foreach (var component in _componentsList)
            {
                if (component is not IMovable moveComponent)
                {
                    continue;
                }
                moveComponent.Move(Time.deltaTime);
            }
        }
    }
}