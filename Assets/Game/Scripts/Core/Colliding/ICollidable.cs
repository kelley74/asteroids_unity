using Game.Core.GameSystems;
using UnityEngine;

namespace Game.Core.Colliding
{
    // ReSharper disable once IdentifierTypo
    public interface ICollidable : IGameEntity
    {
        public enum ColliderType
        {
            Player,
            Bullet,
            Laser,
            Enemy
        }
        // ReSharper disable once IdentifierTypo
        bool CheckCollisions(ICollidable collidable);

        ColliderType GetColliderType();

        Vector3 GetPosition();

        float GetRadius();
    }
}