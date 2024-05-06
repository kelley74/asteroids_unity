using UnityEngine;

namespace Game.Core.Movement
{
    public class TestMoveComponent : IMoveComponent
    {
        public void SetPositionAndRotation(Vector3 position, Quaternion rotation)
        {
            Debug.Log(position);
        }
    }
}
