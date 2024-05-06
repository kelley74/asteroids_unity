using TMPro;
using UnityEngine;

namespace Game.Scripts.UI.UiUtils
{
    public class Vector2ValueToTextSetter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private float _updateSqrMagThreshold = 0.01f;
        
        private Vector2 _value = new Vector2(float.MaxValue,float.MaxValue);
        
        public void SetValue(string format, Vector2 value)
        {
            if ((value - _value).sqrMagnitude > _updateSqrMagThreshold)
            {
                _value = value;
                _text.text = string.Format(format, value);
            }
        }
        
    }
}
