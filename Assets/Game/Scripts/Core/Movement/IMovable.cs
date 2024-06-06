using Game.Core.GameSystems;
using UnityEngine;

namespace Game.Core.Movement
{
    public interface IMovable : IGameComponent
    {
        void Move(float deltaTime);

        Vector3 GetPosition();
        float GetDirectionAngle();
        float GetVelocity();
    }
}