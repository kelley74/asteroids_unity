using System;
using Game.Core.Movement;
using Game.Core.Colliding;
using UnityEngine;

namespace Game.Gameplay.Player
{
    public class LaserCollideController : ICollidable
    {
        private readonly ICollidable.ColliderType _type = ICollidable.ColliderType.Laser;
        private readonly IMovable _movable;
        private readonly Action _onEnemyCollided;

        public LaserCollideController(IMovable movable, Action onEnemyCollided)
        {
            _movable = movable;
            _onEnemyCollided = onEnemyCollided;
        }

        public bool CheckCollisions(ICollidable collidable)
        {
            if (collidable.GetColliderType() == ICollidable.ColliderType.Enemy)
            {
                if ((GetPosition() - collidable.GetPosition()).sqrMagnitude < GetRadius() + collidable.GetRadius())
                {
                    _onEnemyCollided.Invoke();
                    return true;
                }
            }

            return false;
        }

        public ICollidable.ColliderType GetColliderType()
        {
            return _type;
        }

        public Vector3 GetPosition()
        {
            return _movable.GetPosition();
        }

        public float GetRadius()
        {
            return 0.1f; //TODO: Create config
        }
    }
}