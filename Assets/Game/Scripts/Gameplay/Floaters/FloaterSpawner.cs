using System.Collections;
using Game.BindingContainer;
using Game.Services.Pools;
using UnityEngine;

namespace Game.Scripts.Gameplay.Floaters
{
    public class FloaterSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _bulletFloaterPrefab;
        [SerializeField] private GameObject _explosionFloaterPrefab;
        [SerializeField] private float _floaterTime = 1f;
        
        public void SpawnBulletFloater(Vector3 position)
        {
            SpawnFloater(_bulletFloaterPrefab, position, 1f);
        }

        public void SpawnExplosionFloater(Vector3 position)
        {
            SpawnFloater(_explosionFloaterPrefab, position, 0.3f);
        }

        private void SpawnFloater(GameObject prefab, Vector3 position, float duration)
        {
            var pool = DiContainer.Resolve<GoPool>();
            var floater = pool.Pull(prefab);
            floater.transform.position = position;
            StartCoroutine(PlayFloater(pool, floater));
        }

        private IEnumerator PlayFloater(GoPool pool, GameObject floater)
        {
            yield return new WaitForSeconds(_floaterTime);
            pool.Push(floater);
        }
    }
}
