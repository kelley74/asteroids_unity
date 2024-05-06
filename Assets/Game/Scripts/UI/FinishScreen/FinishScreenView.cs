using System;
using System.Collections;
using BigCity.Scripts.UI.Base;
using Game.Scripts.GameData;
using TMPro;
using UnityEngine;

namespace Game.Scripts.UI.FinishScreen
{
    public class FinishScreenView : UiView
    {
        public Action OnQuitButtonPressed;
        
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private GameObject _quitButton;

        private const float QuitButtonShowDelay = 1f;
        
        public override void Show()
        {
            base.Show();
            var data = _model as GameRoundData;
            if (data == null)
            {
                throw new Exception("Game Data has wrong format");
            }
            _score.text = $"Score: {data.EnemiesKilled}";
            StartCoroutine(ActivateQuitButton());
        }

        public void Quit()
        {
            OnQuitButtonPressed?.Invoke();
        }

        private IEnumerator ActivateQuitButton()
        {
            _quitButton.SetActive(false);
            yield return new WaitForSeconds(QuitButtonShowDelay);
            _quitButton.SetActive(true);
        }
    }
}