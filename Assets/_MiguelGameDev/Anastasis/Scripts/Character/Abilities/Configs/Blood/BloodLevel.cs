using System;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    [Serializable]
    public struct BloodLevel
    {
        [SerializeField] private int _maxHealth;
        public int MaxHealth => _maxHealth;
    }
}
