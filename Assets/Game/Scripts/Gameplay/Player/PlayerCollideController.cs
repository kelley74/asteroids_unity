using System;
using Game.Core.Movement;
using Game.Core.Colliding;
using UnityEngine;

namespace Game.Gameplay.Player
{
    public class PlayerCollideController : ICollidable
    {
        private readonly IMovable _movable;
        private readonly Action _onPlayerCollider;
        
        public PlayerCollideController(IMovable movable, Action onPlayerCollider)
        {
            _movable = movable;
            _onPlayerCollider = onPlayerCollider;
        }
        // ReSharper disable once IdentifierTypo
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
