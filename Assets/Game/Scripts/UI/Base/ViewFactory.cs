using System;
using BigCity.Scripts.UI.Base;
using UnityEngine;

namespace Game.UI.Base
{
    public class ViewFactory : MonoBehaviour, IViewFactory
    {
        [SerializeField] private Canvas _mainCanvas;

        public void CreateView(GameObject prefab, Action<IUiView> callback)
        {
            var asset = Instantiate(prefab, _mainCanvas.transform);
            var view = asset.GetComponent<UiView>();
            var viewTransform = view.transform;
            viewTransform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            viewTransform.localScale = Vector3.one;

            var rectTransform = view.GetComponent<RectTransform>();
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
            callback?.Invoke(view);
        }
    }
}