using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI.PrefabScripts
{
    public class FloatSettingArea : MonoBehaviour
    {
        #region Public Methods

        public void SetSlider() => _SetSlider();

        public void SetInputField() => _SetInputField();

        public void SetValue(float value) => _SetValue(value);

        public float GetValue() => _GetValue();

        public void Init(string textName, float maxValue, float minValue = 0f) => _Init(textName, maxValue, minValue);

        #endregion
        
        #region Private Values

        [SerializeField] private TMP_Text text;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Slider slider;
        
        private float _curValue;
        
        #endregion


        #region Private Methods

        private void _Init(string textName, float maxValue, float minValue)
        {
            text.text = textName;
            slider.minValue = minValue;
            slider.maxValue = maxValue;
            
            _curValue = maxValue;
            _SetSlider();
            _SetValue(_curValue);
        }
        
        private void _SetSlider()
        {
            try
            {
                _curValue = (float)Math.Round(float.Parse(inputField.text, CultureInfo.InvariantCulture), 3);
            }
            catch(FormatException e)
            {
                Debug.Log(e);
            }
            
            if (_curValue < slider.minValue)
            {
                _curValue = slider.minValue;
                inputField.text = _curValue.ToString(CultureInfo.InvariantCulture);
            }

            if (_curValue > slider.maxValue)
            {
                _curValue = slider.maxValue;
                inputField.text = _curValue.ToString(CultureInfo.InvariantCulture);
            }
            slider.value = _curValue;
        }

        private void _SetInputField()
        {
            inputField.text = (slider.value).ToString(CultureInfo.InvariantCulture);
        }

        private void _SetValue(float value)
        {
            _curValue = value;
            _SetSlider();
            _SetInputField();
        }

        private float _GetValue()
        {
            return _curValue;
        }

        #endregion
        
    }
}