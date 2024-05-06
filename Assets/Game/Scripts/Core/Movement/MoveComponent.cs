using UnityEngine;

namespace Game.Core.Movement
{
    public class MoveComponent : MonoBehaviour, IMoveComponent
    {
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        public void SetPositionAndRotation(Vector3 position, Quaternion rotation)
        {
            _transform.SetPositionAndRotation(position, rotation);
        }
    }
}
