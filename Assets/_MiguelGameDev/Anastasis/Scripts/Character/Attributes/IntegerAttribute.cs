using Sirenix.OdinInspector;
using System;

namespace MiguelGameDev.Anastasis
{
    public class IntegerAttribute
    {
        [ShowInInspector] private int _value;
        public int Value {
            get => _value;
            set {
                if (_value == value)
                {
                    return;
                }

                int diff = value - _value;
                _value = value;
                _onValueChange?.Invoke(diff);
            }
        }

        private event Action<int> _onValueChange;

        public IntegerAttribute(int value)
        {
            _value = value;
        }

        public void Subscribe(Action<int> onValueChange)
        {
            _onValueChange += onValueChange;
        }

        public void Unsubscribe(Action<int> onValueChange)
        {
            _onValueChange -= onValueChange;
        }
    }
}
