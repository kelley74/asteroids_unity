using TMPro;
using UnityEngine;

namespace Game.Scripts.UI.UiUtils
{
    public class FloatValueToTextSetter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private float _updateThreshold = 0.01f;
        
        private float _value = float.MaxValue;
        
        public void SetValue(string format, float value)
        {
            if (Mathf.Abs(value - _value) > _updateThreshold)
            {
                _value = value;
                _text.text = string.Format(format, value);
            }
        }
    }
}
