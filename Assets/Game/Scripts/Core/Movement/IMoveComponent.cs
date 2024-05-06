using UnityEngine;

namespace Game.Core.Movement
{
    public interface IMoveComponent
    {
        void SetPositionAndRotation(Vector3 position, Quaternion rotation);
    }
}