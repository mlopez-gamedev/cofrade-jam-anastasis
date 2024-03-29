using System;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    [Serializable]
    public struct BlessingLevel
    {
        [SerializeField] int _damage;
        [SerializeField] float _cooldown;
        [SerializeField] float _size;

        public int Damage => _damage;
        public float Cooldown => _cooldown;
        public float Size => _size;
    }
}
