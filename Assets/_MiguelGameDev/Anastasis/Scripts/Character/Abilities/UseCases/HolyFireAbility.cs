using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

namespace MiguelGameDev.Anastasis
{
    public class HolyFireAbility : Ability
    {
        [ShowInInspector] private HolyFireLevel[] _levels;
        private readonly HolyFireAvatar _avatarPrefab;
        private readonly ObjectPool<HolyFireAvatar> _avatarPool;

        private readonly FloatAttribute _cooldown;
        private readonly FloatAttribute _maxDistance;
        private readonly IntegerAttribute _damage;
        private readonly FloatAttribute _size;
        private readonly FloatAttribute _duration;

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

            _avatarPool = new ObjectPool<HolyFireAvatar>(CreateAvatar, TakeAvatarFromPool, ReturnAvatarToPool);
        }

        private HolyFireAvatar CreateAvatar()
        {
            var avatar = Object.Instantiate(_avatarPrefab);
            avatar.gameObject.SetActive(false);
            avatar.Setup(this);
            return avatar;
        }

        private void TakeAvatarFromPool(HolyFireAvatar avatar)
        {
            var position = _owner.Transform.position;
            var randomOffset = Random.insideUnitCircle * _maxDistance.Value;
            position.x += randomOffset.x;
            position.z += randomOffset.y;

            avatar.Init(position);
        }

        private void ReturnAvatarToPool(HolyFireAvatar avatar)
        {
            avatar.Finish();
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

            _avatarPool.Get();
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

            if (_owner.TeamId == damageReceiver.TeamId)
            {
                return;
            }

            damageReceiver.TakeDamage(new DamageInfo(_owner.Transform, _owner.TeamId, _damage.Value));
        }
    }
}
