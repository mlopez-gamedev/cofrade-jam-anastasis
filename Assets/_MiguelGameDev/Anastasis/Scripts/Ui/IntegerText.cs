using System;
using TMPro;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class IntegerText : MonoBehaviour
    {
        [SerializeField] TMP_Text _text;

        private string _format;
        private IntegerAttribute _value;


        public void Setup(string format, IntegerAttribute value) 
        {
            _format = format;
            _value = value;

            SetText();
            _value.Subscribe(OnValueChange);
        }

        private void OnValueChange(int _)
        {
            SetText();
        }

        private void SetText()
        {
            _text.text = String.Format(_format, _value.Value);
        }

        private void OnDestroy()
        {
            _value.Unsubscribe(OnValueChange);
        }
    }
}
