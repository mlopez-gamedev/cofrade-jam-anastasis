using System;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{

    [Serializable]
    public struct HolySpiritLevel
    {
        [SerializeField] HolySpiritLevelUnit[] _units;
        
        [SerializeField] int _damage;
        public HolySpiritLevelUnit[] Units => _units;
        public int Damage => _damage;
    }
}
