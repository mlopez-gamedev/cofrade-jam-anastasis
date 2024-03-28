using Sirenix.OdinInspector;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

namespace MiguelGameDev.Anastasis
{
    public class HolyFireAbility : Ability
    {
        [ShowInInspector] private HolyFireLevel[] _levels;
        private HolyFireAvatar _avatarPrefab;

        private FloatAttribute _cooldown;
        private FloatAttribute _maxDistance;
        private IntegerAttribute _damage;
        private FloatAttribute _size;
        private FloatAttribute _duration;

        public override int MaxLevel => _levels.Length - 1;

        public IntegerAttribute Damage => _damage;
        public FloatAttribute Size => _size;
        public FloatAttribute Duration => _duration;

        private float _nextHolyFireTime;

        public HolyFireAbility(CharacterAbilities owner, HolyFireAbilityConfig config) : base(owner, config)
        {
            _avatarPrefab = config.HolyFireAvatar;
            _levels = config.Levels;

            _cooldown = new FloatAttribute(_levels[0].Cooldown);
            _maxDistance = new FloatAttribute(_levels[0].MaxDistance);
            _damage = new IntegerAttribute(_levels[0].Damage);
            _size = new FloatAttribute(_levels[0].Size);
            _duration = new FloatAttribute(_levels[0].Duration);
        }

        protected override void ApplyUpgrade()
        {
            _cooldown.Value = _levels[_currentLevel].Cooldown;
            _maxDistance.Value = _levels[_currentLevel].MaxDistance;
            _damage.Value = _levels[_currentLevel].Damage;
            _size.Value = _levels[_currentLevel].Size;
            _duration.Value = _levels[_currentLevel].Duration;

            if (_currentLevel == 1)
            {
                _nextHolyFireTime = Time.time + _cooldown.Value / 2;
            }
        }

        public override bool Update()
        {
            if (Time.time < _nextHolyFireTime)
            {
                return false;
            }

            var position = _owner.Transform.position;
            var randomOffset = Random.insideUnitCircle * _maxDistance.Value;
            position.x += randomOffset.x;
            position.z += randomOffset.y;

            var holyFire = Object.Instantiate(_avatarPrefab, position, Quaternion.identity);
            holyFire.Setup(this);

            _nextHolyFireTime = Time.time + _cooldown.Value;
            return true;
        }

        public void TryMakeDamage(Collider other)
        {
            CharacterDamageReceiver damageReceiver = other.GetComponent<CharacterDamageReceiver>();
            if (damageReceiver == null)
            {
                return;
            }

            // damageReceiver.TakeDamage(new DamageInfo(_damage.Value, 0, Vector2.zero));
        }
    }
}
