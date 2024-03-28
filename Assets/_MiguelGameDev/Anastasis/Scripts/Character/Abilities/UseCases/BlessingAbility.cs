using Sirenix.OdinInspector;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class BlessingAbility : Ability
    {
        [ShowInInspector] private BlessingLevel[] _levels;
        private BlessingAvatar _avatarPrefab;

        private BlessingAvatar _avatar;

        private FloatAttribute _damageMultiplier;
        private IntegerAttribute _damage;
        private FloatAttribute _size;

        public IntegerAttribute Damage => _damage;
        public FloatAttribute Size => _size;

        public override int MaxLevel => _levels.Length - 1;

        public BlessingAbility(CharacterAbilities owner, BlessingAbilityConfig config) : base(owner, config)
        {
            _avatarPrefab = config.Avatar;
            _levels = config.Levels;

            _damageMultiplier = owner.PlayerAttributes.DamageMultiplier;
            _damage = new IntegerAttribute(Mathf.CeilToInt(_levels[0].Damage * _damageMultiplier.Value));
            _size = new FloatAttribute(_levels[0].Size);
            _damageMultiplier.Subscribe(OnDamageMultiplierChange);
        }

        private void OnDamageMultiplierChange(float diff)
        {
            _damage.Value = Mathf.CeilToInt(_levels[_currentLevel].Damage * _damageMultiplier.Value);
        }

        protected override void ApplyUpgrade()
        {
            _damage.Value = Mathf.CeilToInt(_levels[_currentLevel].Damage * _damageMultiplier.Value);
            _size.Value = _levels[_currentLevel].Size;

            if (_currentLevel == 1)
            {
                _avatar = Object.Instantiate(_avatarPrefab, _owner.Transform);
                _avatar.Setup(this);
            }
        }

        public override bool Update()
        {
            _avatar.Tick();
            return false;
        }

        public void TryMakeDamage(Collider other)
        {
            CharacterDamageReceiver damageReceiver = other.GetComponent<CharacterDamageReceiver>();
            if (damageReceiver == null)
            {
                return;
            }

            if (damageReceiver.TeamId == _owner.TeamId)
            {
                return;
            }

            damageReceiver.TakeDamage(new DamageInfo(_owner.Transform, _owner.TeamId, _damage.Value));
        }

        internal override void Release()
        {
            _damageMultiplier.Unsubscribe(OnDamageMultiplierChange);
        }
    }
}
