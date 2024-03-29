using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class DamageOverTime
    {
        private readonly CharacterDamageReceiver _damageReceiver;
        private readonly Transform _instigator;
        private readonly Transform _cause;
        private readonly int _teamId;
        private readonly IntegerAttribute _damage;
        private readonly FloatAttribute _damageCooldown;
        private float _nextDamage;

        public int TeamId => -1; // Environment team

        public DamageOverTime(CharacterDamageReceiver damageReceiver, Transform instigator, Transform cause, int teamId, IntegerAttribute damage, FloatAttribute damageCooldown)
        {
            _damageReceiver = damageReceiver;
            _instigator = instigator;
            _cause = cause;
            _teamId = teamId;
            _damage = damage;
            _damageCooldown = damageCooldown;
        }


        public bool TryMakeDamage()
        {
            if (Time.time < _nextDamage)
            {
                return false;
            }

            var damageInfo = new DamageInfo(_instigator, _cause, _teamId, _damage.Value);
            _damageReceiver.TryTakeDamage(damageInfo);
            _nextDamage = Time.time + _damageCooldown.Value;
            return true;
        }
    }
}
