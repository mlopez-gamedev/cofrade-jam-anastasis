﻿using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class IntegerLinearInterpolator
    {
        private readonly int _minLevel;
        private readonly int _maxLevel;

        private readonly int _minValue;
        private readonly float _valueFactor;

        public IntegerLinearInterpolator(int minLevel, int maxLevel, int minValue, int maxValue)
        {
            _minLevel = minLevel;
            _maxLevel = maxLevel;
            _minValue = minValue;

            int levelDiff = maxLevel - minLevel;
            int valueDiff = maxValue - minValue;

            _valueFactor = (float)valueDiff / levelDiff;
        }

        public int GetValue(int level)
        {
            level = (level - _minLevel);
            return _minValue + Mathf.RoundToInt(level * _valueFactor);
        }

        public int GetInverseValue(int level)
        {
            level = (level - _minLevel);
            level = _maxLevel - level;
            return _minValue + Mathf.RoundToInt(level * _valueFactor);
        }
    }
}