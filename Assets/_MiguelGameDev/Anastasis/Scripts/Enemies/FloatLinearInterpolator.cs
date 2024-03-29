namespace MiguelGameDev.Anastasis
{

    public class FloatLinearInterpolator
    {
        private readonly float _minLevel;
        private readonly float _maxLevel;

        private readonly float _minValue;
        private readonly float _valueFactor;

        public FloatLinearInterpolator(float minLevel, float maxLevel, float minValue, float maxValue)
        {
            _minLevel = minLevel;
            _maxLevel = maxLevel;
            _minValue = minValue;

            float levelDiff = maxLevel - minLevel;
            float valueDiff = maxValue - minValue;

            _valueFactor = valueDiff / levelDiff;
        }

        public float GetValue(float level)
        {
            level = (level - _minLevel);
            return _minValue + level * _valueFactor;
        }

        public float GetInverseValue(float level)
        {
            level = (level - _minLevel);
            level = _maxLevel - level;
            return _minValue + level * _valueFactor;
        }
    }
}