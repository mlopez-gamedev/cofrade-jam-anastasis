using Sirenix.OdinInspector;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

namespace MiguelGameDev.Anastasis
{

    public class BlessingAbility : Ability
    {
        [ShowInInspector] private BlessingLevel[] _levels;
        private BlessingAvatar _avatarPrefab;

        private BlessingAvatar _avatar;

        private FloatAttribute _damageMultiplier;
        private IntegerAttribute _damage;
        private FloatAttribute _cooldown;
        private FloatAttribute _size;

        public IntegerAttribute Damage => _damage;
        public FloatAttribute Cooldown => _cooldown;
        public FloatAttribute Size => _size;

        public override int MaxLevel => _levels.Length - 1;

        private Dictionary<Collider, DamageOverTime> _blessingCharacters;

        public BlessingAbility(CharacterAbilities owner, BlessingAbilityConfig config) : base(owner, config)
        {
            _avatarPrefab = config.Avatar;
            _levels = config.Levels;

            _blessingCharacters = new Dictionary<Collider, DamageOverTime>();

            _damageMultiplier = owner.PlayerAttributes.DamageMultiplier;
            _damage = new IntegerAttribute(Mathf.CeilToInt(_levels[0].Damage * _damageMultiplier.Value));
            _cooldown = new FloatAttribute(_levels[0].Cooldown);
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
            _cooldown.Value = _levels[_currentLevel].Cooldown;
            _size.Value = _levels[_currentLevel].Size;

            if (_currentLevel == 1)
            {
                _avatar = Object.Instantiate(_avatarPrefab, _owner.Transform);
                _avatar.Setup(this);
            }
        }

        public override bool Update()
        {
            _avatar?.Tick();
            bool didDamage = false;
            foreach (DamageOverTime damageOverTime in _blessingCharacters.Values)
            {
                if (damageOverTime.TryMakeDamage())
                {
                    didDamage = true;
                }
            }
            return didDamage;
        }

        public void TryAddTarget(Collider other)
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

            Assert.IsFalse(_blessingCharacters.ContainsKey(other));

            var damageOverTime = new DamageOverTime(damageReceiver, _owner.Transform, _owner.Transform, _owner.TeamId, _damage, _cooldown);

            _blessingCharacters.Add(other, damageOverTime);
        }

        public void TryRemoveTarget(Collider other)
        {
            if (!_blessingCharacters.ContainsKey(other))
            {
                return;
            }

            _blessingCharacters.Remove(other);
        }

        internal override void Release()
        {
            _damageMultiplier.Unsubscribe(OnDamageMultiplierChange);
        }
    }
}
