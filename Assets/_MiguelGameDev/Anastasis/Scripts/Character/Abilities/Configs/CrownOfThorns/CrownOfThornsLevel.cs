using System;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    [Serializable]
    public struct CrownOfThornsLevel
    {
        [SerializeField] float _stuntDuration;
        [SerializeField] float _pushForce;
        [SerializeField] float _minDamageMultiplier;
        [SerializeField] float _maxDamageMultiplier;

        public float MinDamageMultiplier => _minDamageMultiplier;
        public float MaxDamageMultiplier => _maxDamageMultiplier;
    }
}
