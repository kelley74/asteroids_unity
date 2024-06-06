using UnityEngine;

namespace Game.Core.Movement
{
    public interface IMoveEntity
    {
        void SetPositionAndRotation(Vector3 position, Quaternion rotation);
    }
}