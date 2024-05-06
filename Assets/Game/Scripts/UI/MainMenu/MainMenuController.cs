using Game.BindingContainer;
using Game.Gameplay.Master;
using Game.UI.Base;
using UnityEngine;

namespace Game.UI.MainMenu
{
    public class MainMenuController : UiController<MainMenuView>
    {
        public MainMenuController(SiblingSorter siblingSorter, IViewFactory factory, GameObject viewPrefab) : base(
            siblingSorter, factory, viewPrefab)
        {
            
        }

        protected override void OnOpen()
        {
            _view.OnButtonPlayPressed += OnButtonPlayPressed;
        }

        protected override void OnClose()
        {
            _view.OnButtonPlayPressed -= OnButtonPlayPressed;
        }

        private void OnButtonPlayPressed()
        {
            Close();
            DiContainer.Resolve<GameMaster>().StartGame();
        }

        protected override int SiblingIndex => 1;
    }
}