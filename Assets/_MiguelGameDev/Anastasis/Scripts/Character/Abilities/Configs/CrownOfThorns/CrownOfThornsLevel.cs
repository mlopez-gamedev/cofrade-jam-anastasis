using System;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    [Serializable]
    public struct CrownOfThornsLevel
    {
        [SerializeField] float _minDamageMultiplier;
        [SerializeField] float _maxDamageMultiplier;
        [SerializeField] float _stuntDuration;
        [SerializeField] float _pushForce;

        public float MinDamageMultiplier => _minDamageMultiplier;
        public float MaxDamageMultiplier => _maxDamageMultiplier;
        public float StuntDuration => _stuntDuration;
        public float PushForce => _pushForce;
    }
}
