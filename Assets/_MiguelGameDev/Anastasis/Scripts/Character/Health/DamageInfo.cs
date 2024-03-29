using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public readonly struct DamageInfo
    {
        public Transform Instigator { get; } // Character transform
        public Transform Cause { get; } // Collider transform (could be a projectile or the character)
        public int TeamId { get; }
        public int Damage { get; }
        public float StuntDuration { get; }
        public Vector2 PushForce { get; }

        public DamageInfo(Transform instigator, int teamId, int damage = 0, float stunt = 0, Vector2 pushForce = default)
        {
            Instigator = instigator;
            Cause = instigator;
            TeamId = teamId;
            Damage = damage;
            StuntDuration = stunt;
            PushForce = pushForce;
        }

        public DamageInfo(Transform instigator, Transform cause, int teamId, int damage = 0, float stunt = 0, Vector2 pushForce = default)
        {
            Instigator = instigator;
            Cause = cause;
            TeamId = teamId;
            Damage = damage;
            StuntDuration = stunt;
            PushForce = pushForce;
        }
    }
}