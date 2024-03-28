using System;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{


    [Serializable]
    public struct BlessingLevel
    {
        [SerializeField] int _damage;
        [SerializeField] float _size;

        public int Damage => _damage;
        public float Size => _size;
    }
}
