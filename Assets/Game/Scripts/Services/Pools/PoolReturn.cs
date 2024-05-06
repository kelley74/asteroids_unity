using UnityEngine;

namespace Game.Services.Pools
{
    public class PoolReturn : MonoBehaviour
    {
        public GameObject Prefab { get; private set; }
        public void Init(GameObject prefab)
        {
            Prefab = prefab;
        }
    }
}