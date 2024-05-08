using System.Collections;
using Game.BindingContainer;
using Game.Services.Pools;
using UnityEngine;

namespace Game.Gameplay.Floaters
{
    public class FloaterSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _bulletFloaterPrefab;
        [SerializeField] private GameObject _explosionFloaterPrefab;
        [SerializeField] private float _bulletTime = 1f;
        [SerializeField] private float _explosionTime = 0.75f;
        
        public void SpawnBulletFloater(Vector3 position)
        {
            SpawnFloater(_bulletFloaterPrefab, position, _bulletTime);
        }

        public void SpawnExplosionFloater(Vector3 position)
        {
            SpawnFloater(_explosionFloaterPrefab, position, _explosionTime);
        }

        private void SpawnFloater(GameObject prefab, Vector3 position, float duration)
        {
            var pool = DiContainer.Resolve<GoPool>();
            var floater = pool.Pull(prefab);
            floater.transform.position = position;
            StartCoroutine(PlayFloater(pool, floater, duration));
        }

        private IEnumerator PlayFloater(GoPool pool, GameObject floater, float duration)
        {
            yield return new WaitForSeconds(duration);
            pool.Push(floater);
        }
    }
}
