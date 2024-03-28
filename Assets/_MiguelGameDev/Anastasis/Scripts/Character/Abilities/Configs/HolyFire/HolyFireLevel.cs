using System;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    [Serializable]
    public struct HolyFireLevel
    {
        [SerializeField] float _cooldown;
        [SerializeField] float _maxDistance;
        [SerializeField] int _damage;
        [SerializeField] float _size;

        [SerializeField] float _duration;

        public float Cooldown => _cooldown;
        public float MaxDistance => _maxDistance;
        public int Damage => _damage;
        public float Size => _size;
        public float Duration => _duration;
    }
}
