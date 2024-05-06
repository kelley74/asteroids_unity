using BigCity.Scripts.UI.Base;
using UnityEngine;

namespace Game.UI.Base
{
    public class ViewProvider : MonoBehaviour
    {
        [SerializeField] private UiView[] _views;

        public UiView GetView(string viewName)
        {
            foreach (var view in _views)
            {
                if (view.name == viewName)
                {
                    return view;
                }
            }

            return null;
        }
    }
}
