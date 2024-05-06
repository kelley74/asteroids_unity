using BigCity.Scripts.UI.Base;
using Game.Scripts.GameData;
using Game.Scripts.UI.UiUtils;
using UnityEngine;

namespace Game.Scripts.UI.GameplayScreen
{
    public class GameplayScreenView : UiView
    {
        [SerializeField] private FloatValueToTextSetter _velocitySetter;
        [SerializeField] private FloatValueToTextSetter _angleSetter;
        [SerializeField] private Vector2ValueToTextSetter _coordsSetter;

        public override void Show()
        {
            base.Show();
            var model = (GameplayData)_model;
            model.OnChange += UpdateModel;
        }

        public override void Hide()
        {
            base.Hide();
            var model = (GameplayData)_model;
            model.OnChange -= UpdateModel;
        }

        private void UpdateModel()
        {
            var model = (GameplayData)_model;

            _velocitySetter.SetValue("Speed: {0:0.00} u/s", model.Velocity * Application.targetFrameRate);
            _angleSetter.SetValue("Angle: {0:0.00}", model.RotationAngle);
            _coordsSetter.SetValue("Coords: {0}", model.Coordinates);
        }
    }
}