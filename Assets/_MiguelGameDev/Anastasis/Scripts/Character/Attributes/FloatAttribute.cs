using Sirenix.OdinInspector;
using System;

namespace MiguelGameDev.Anastasis
{
public class FloatAttribute
    {
        [ShowInInspector] private float _value;
        public float Value {
            get => _value; 
            set {
                if (_value == value)
                {
                    return;
                }

                float diff = value - _value;
                _value = value;
                _onValueChange?.Invoke(diff);
            }
        }

        private event Action<float> _onValueChange;

        public FloatAttribute(float value)
        {
            _value = value;
        }

        public void Subscribe(Action<float> onValueChange)
        {
            _onValueChange += onValueChange;
        }

        public void Unsubscribe(Action<float> onValueChange)
        {
            _onValueChange -= onValueChange;
        }
    }
}