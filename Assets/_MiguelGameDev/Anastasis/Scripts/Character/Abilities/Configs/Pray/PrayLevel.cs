using System;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    [Serializable]
    public struct PrayLevel
    {
        [SerializeField] private float _cooldown;
        [SerializeField] private int _heal;

        public float Cooldown => _cooldown;
        public int Heal => _heal;
    }
}
