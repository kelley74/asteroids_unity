using Game.UI.Base;
using UnityEngine;

namespace Game.Scripts.UI.FinishScreen
{
    public class FinishScreenController : UiController<FinishScreenView>
    {
        public FinishScreenController(SiblingSorter siblingSorter, IViewFactory factory, GameObject viewPrefab) : base(siblingSorter, factory, viewPrefab)
        {
            
        }

        protected override int SiblingIndex => 12;

        protected override void OnOpen()
        {
            _view.OnQuitButtonPressed += OnQuitButtonPressed;
        }

        protected override void OnClose()
        {
            _view.OnQuitButtonPressed -= OnQuitButtonPressed;
        }

        private void OnQuitButtonPressed()
        {
            Close();
        }
    }
}
