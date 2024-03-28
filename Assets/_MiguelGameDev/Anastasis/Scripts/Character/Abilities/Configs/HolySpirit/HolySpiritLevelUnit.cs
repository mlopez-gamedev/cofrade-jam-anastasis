using System;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    [Serializable]
    public struct HolySpiritLevelUnit
    {
        [SerializeField] float _rotateAngularSpeed;
        [SerializeField] float _cycleDuration;
        [SerializeField] float _maxDistance;

        public float RotateAngularSpeed => _rotateAngularSpeed;
        public float CycleDuration => _cycleDuration;
        public float MaxDistance => _maxDistance;
    }
}
