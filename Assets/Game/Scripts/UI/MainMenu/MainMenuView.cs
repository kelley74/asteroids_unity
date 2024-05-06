using System;
using BigCity.Scripts.UI.Base;

namespace Game.UI.MainMenu
{
    public class MainMenuView : UiView
    {
        public Action OnButtonPlayPressed;
        
        public void Play()
        {
            OnButtonPlayPressed?.Invoke();
        }
    }
}