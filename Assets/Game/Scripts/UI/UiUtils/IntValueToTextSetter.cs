using TMPro;
using UnityEngine;

namespace Game.Scripts.UI.UiUtils
{
    public class IntValueToTextSetter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private int _updateThreshold = 1;
        
        private int _value = int.MaxValue;
        
        public void SetValue(string format, int value)
        {
            if (Mathf.Abs(value - _value) >= _updateThreshold)
            {
                _value = value;
                _text.text = string.Format(format, value);
            }
        }
    }
}
