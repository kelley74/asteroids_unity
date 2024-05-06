using Game.BindingContainer;
using Game.Gameplay.Master;
using Game.Scripts.UI.FinishScreen;
using Game.Scripts.UI.GameplayScreen;
using Game.UI.Base;
using Game.UI.MainMenu;
using UnityEngine;

namespace Game.UI.Installer
{
    public class UiInstaller : BindingInstaller
    {
        [SerializeField] private ViewFactory _viewFactory;
        [SerializeField] private GameObject _mainMenuView;
        [SerializeField] private GameObject _finishScreen;
        [SerializeField] private GameObject _gameplayScreen;
        
        public override void Bind()
        {
            var siblingSorter = new SiblingSorter();

            var mainMenuController = new MainMenuController(siblingSorter, _viewFactory, _mainMenuView);
            var finishScreen = new FinishScreenController(siblingSorter, _viewFactory, _finishScreen);
            var gameplayScreen = new GameplayScreenController(siblingSorter, _viewFactory, _gameplayScreen);
            
            AddBinding(mainMenuController);
            AddBinding(finishScreen);
            AddBinding(gameplayScreen);
            
            StartCoroutine(mainMenuController.Open());
            
            var gameMaster = DiContainer.Resolve<GameMaster>();

            gameMaster.OnGameStarted += (gameplayData) =>
            {
                StartCoroutine(gameplayScreen.Open(gameplayData));
            };
            
            gameMaster.OnGameFinished += (gameRoundData) =>
            {
                gameplayScreen.Close();
                StartCoroutine(mainMenuController.Open());
                StartCoroutine(finishScreen.Open(gameRoundData));
            };
            
        }
    }
}
