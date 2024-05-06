using System.Collections.Generic;
using UnityEngine;

namespace Game.Services.Pools
{
    public class GoPool : MonoBehaviour
    {
        private Dictionary<GameObject, List<GameObject>> _pool = new Dictionary<GameObject, List<GameObject>>();
        
        public GameObject Pull(GameObject prefab)
        {
            if (!_pool.TryGetValue(prefab, out var list))
            {
                list = new List<GameObject>();
                _pool.Add(prefab, list);
            }

            if (list.Count > 0)
            {
                var obj = list[0];
                list.Remove(obj);
                obj.SetActive(true);
                return obj;
            }

            var newObj = Instantiate(prefab);
            newObj.AddComponent<PoolReturn>().Init(prefab);
            newObj.SetActive(true);
            return newObj;
        }

        public void Push(GameObject obj)
        {
            obj.SetActive(false);
            var prefab = obj.GetComponent<PoolReturn>().Prefab;
            _pool[prefab].Add(obj);
        }
    }
}
