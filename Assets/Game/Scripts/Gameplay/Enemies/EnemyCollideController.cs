using System;
using Game.BindingContainer;
using Game.Core.Movement;
using Game.Scripts.Core.Colliding;
using UnityEngine;

namespace Game.Gameplay.Enemies
{
    public class EnemyCollideController : ICollidable
    {
        private ICollidable.ColliderType _type = ICollidable.ColliderType.Enemy;
        private IMovable _movable;
        private Action _onBulletCollided;

        public EnemyCollideController(IMovable movable, Action onBulletCollided)
        {
            _movable = movable;
            _onBulletCollided = onBulletCollided;
        }

        public bool CheckCollisions(ICollidable collidable)
        {
            switch (collidable.GetColliderType())
            {
                case ICollidable.ColliderType.Bullet:
                    if ((GetPosition() - collidable.GetPosition()).sqrMagnitude < GetRadius() + collidable.GetRadius())
                    {
                        _onBulletCollided?.Invoke();
                        return true;
                    }
                    break;
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
            return 0.25f;
        }
    }
}
