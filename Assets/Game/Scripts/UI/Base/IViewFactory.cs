using System;
using BigCity.Scripts.UI.Base;
using UnityEngine;

namespace Game.UI.Base
{
    public interface IViewFactory
    {
        void CreateView(GameObject viewPrefab, Action<IUiView> callback);
    }
}