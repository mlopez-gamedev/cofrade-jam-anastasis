using System;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    [Serializable]
    public struct HostLevel
    {
        [SerializeField] private float _damageMultiplier;

        public float DamageMultiplier => _damageMultiplier;
    }
}
