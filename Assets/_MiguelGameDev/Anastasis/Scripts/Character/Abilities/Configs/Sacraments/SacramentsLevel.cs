using System;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    [Serializable]
    public partial struct SacramentsLevel
    {
        [SerializeField] int _amount;
        [SerializeField] float _cooldown;
        [SerializeField] float _range;
        [SerializeField] float _duration;
        [SerializeField] int _damage;
        [SerializeField] float _stuntDuration;
        [SerializeField] float _pushForce;

        public int Amount => _amount;
        public float Cooldown => _cooldown;
        public float Range => _range;
        public int Damage => _damage;
        public float Duration => _duration;
        public float StuntDuration => _stuntDuration;
        public float PushForce => _pushForce;
    }
}
