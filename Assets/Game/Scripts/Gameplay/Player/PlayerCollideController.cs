using System;
using Game.Core.Movement;
using Game.Scripts.Core.Colliding;
using UnityEngine;

namespace Game.Gameplay.Player
{
    public class PlayerCollideController : ICollidable
    {
        private IMovable _movable;
        private Action _onPlayerCollider;
        
        public PlayerCollideController(IMovable movable, Action onPlayerCollider)
        {
            _movable = movable;
            _onPlayerCollider = onPlayerCollider;
        }
        public bool CheckCollisions(ICollidable collidable)
        {
            if (collidable.GetColliderType() == ICollidable.ColliderType.Enemy)
            {
                if ((GetPosition() - collidable.GetPosition()).sqrMagnitude < GetRadius() + collidable.GetRadius())
                {
                    _onPlayerCollider?.Invoke();
                    return true;
                }
            }

            return false;
        }

        public ICollidable.ColliderType GetColliderType()
        {
            return ICollidable.ColliderType.Player;
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
