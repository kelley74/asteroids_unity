using Game.UI.Base;
using UnityEngine;

namespace Game.Scripts.UI.GameplayScreen
{
    public class GameplayScreenController : UiController<GameplayScreenView>
    {
        public GameplayScreenController(SiblingSorter siblingSorter, IViewFactory factory, GameObject viewPrefab) :
            base(siblingSorter, factory, viewPrefab)
        {
        }

        protected override int SiblingIndex => 2;
    }
}