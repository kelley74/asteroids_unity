using System;
using Game.BindingContainer;
using Game.Core.Life;
using Game.Core.Movement;
using Game.Core.Colliding;
using UnityEngine;

namespace Game.Gameplay.Enemies
{
    public class EnemyCollideController : ICollidable
    {
        private ICollidable.ColliderType _type = ICollidable.ColliderType.Enemy;
        private IMovable _movable;
        private Action<DeadReason> _onBulletCollided;
        private float _radius;

        public EnemyCollideController(IMovable movable, Action<DeadReason> onBulletCollided, EnemyConfig config)
        {
            _movable = movable;
            _onBulletCollided = onBulletCollided;
            _radius = config.Radius;
        }

        public bool CheckCollisions(ICollidable collidable)
        {
            switch (collidable.GetColliderType())
            {
                case ICollidable.ColliderType.Bullet:
                    if ((GetPosition() - collidable.GetPosition()).sqrMagnitude < GetRadius() + collidable.GetRadius())
                    {
                        _onBulletCollided?.Invoke(DeadReason.Bullet);
                        return true;
                    }
                    break;
                case ICollidable.ColliderType.Laser:
                    if ((GetPosition() - collidable.GetPosition()).sqrMagnitude < GetRadius() + collidable.GetRadius())
                    {
                        _onBulletCollided?.Invoke(DeadReason.Laser);
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
            return _radius;
        }
    }
}
