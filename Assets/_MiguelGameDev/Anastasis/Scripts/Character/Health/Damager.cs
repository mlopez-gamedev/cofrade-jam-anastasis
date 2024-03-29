using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class Damager : MonoBehaviour
    {
        private Transform _owner;
        private int _teamId;
        private IntegerAttribute _damage;

        public void Setup(Transform owner, int teamId, IntegerAttribute damage)
        {
            _owner = owner;
            _teamId = teamId;
            _damage = damage;
        }

        private void OnTriggerEnter(Collider other)
        {
            CharacterDamageReceiver damageReceiver = other.GetComponent<CharacterDamageReceiver>();
            if (damageReceiver == null)
            {
                return;
            }

            if (_teamId == damageReceiver.TeamId)
            {
                return;
            }

            damageReceiver.TryTakeDamage(new DamageInfo(_owner, _teamId, _damage.Value));
        }
    }
}