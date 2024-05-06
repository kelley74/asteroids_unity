using System;
using System.Collections;
using UnityEngine;

namespace Game.Scripts.Services.Trail
{
    public class ClearOnEnable : MonoBehaviour
    {
        [SerializeField] private TrailRenderer _trailRenderer;

        private void OnEnable()
        {
            _trailRenderer.emitting = false;
            StartCoroutine(Activate());
        }

        private IEnumerator Activate()
        {
            yield return null;
            _trailRenderer.Clear();
            _trailRenderer.emitting = true;
        }

        private void OnDisable()
        {
            
            _trailRenderer.emitting = false;
        }
    }
}
