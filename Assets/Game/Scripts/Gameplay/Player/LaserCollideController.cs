using System;
using Game.Core.Movement;
using Game.Scripts.Core.Colliding;
using UnityEngine;

namespace Game.Gameplay.Player
{
    public class LaserCollideController : ICollidable
    {
        private ICollidable.ColliderType _type = ICollidable.ColliderType.Laser;
        private IMovable _movable;
        private Action _onEnemyCollided;

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
            return 0.1f;
        }
    }
}