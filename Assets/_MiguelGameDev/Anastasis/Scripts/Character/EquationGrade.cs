using System;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    [Serializable]
    public class EquationGrade
    {
        [SerializeField] float _multiplier;
        [SerializeField] float _power;

        public float Multiplier => _multiplier;
        public float Power => _power;
    }
}