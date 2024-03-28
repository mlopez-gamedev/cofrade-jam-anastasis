using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public readonly struct DamageInfo
    {
        public Transform Instigator { get; }
        public int TeamId { get; }
        public int Damage { get; }
        public float StuntDuration { get; }
        public Vector2 PushForce { get; }

        public DamageInfo(Transform instigator, int teamId, int damage = 0, float stunt = 0, Vector2 pushForce = default)
        {
            Instigator = instigator;
            TeamId = teamId;
            Damage = damage;
            StuntDuration = stunt;
            PushForce = pushForce;
        }
    }
}