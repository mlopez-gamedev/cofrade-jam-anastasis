using System;
using TMPro;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class IntegerProgressText : MonoBehaviour
    {
        [SerializeField] TMP_Text _text;

        private string _format;
        private IntegerAttribute _value;
        private IntegerAttribute _maxValue;

        public void Setup(string format, IntegerAttribute value, IntegerAttribute maxValue)
        {
            _format = format;
            _value = value;
            _maxValue = maxValue;

            SetText();
            _value.Subscribe(OnValueChange);
            _maxValue.Subscribe(OnMaxValueChange);
        }

        private void OnValueChange(int _)
        {
            SetText();
        }

        private void OnMaxValueChange(int _)
        {
            SetText();
        }

        private void SetText()
        {
            _text.text = String.Format(_format, _value.Value, _maxValue.Value);
        }

        private void OnDestroy()
        {
            _value.Unsubscribe(OnValueChange);
            _maxValue.Unsubscribe(OnMaxValueChange);
        }
    }
}
