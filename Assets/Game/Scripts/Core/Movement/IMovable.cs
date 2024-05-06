using Game.Core.GameSystems;
using UnityEngine;

namespace Game.Core.Movement
{
    public interface IMovable : IGameEntity
    {
        void Move(float deltaTime);

        Vector3 GetPosition();
        float GetDirectionAngle();
        IMoveComponent GetMoveComponent();
        float GetVelocity();
    }
}