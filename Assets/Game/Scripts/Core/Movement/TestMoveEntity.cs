using UnityEngine;

namespace Game.Core.Movement
{
    public class TestMoveEntity : IMoveEntity
    {
        public void SetPositionAndRotation(Vector3 position, Quaternion rotation)
        {
            Debug.Log(position);
        }
    }
}
