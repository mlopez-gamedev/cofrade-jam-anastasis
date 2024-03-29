using Sirenix.OdinInspector;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

namespace MiguelGameDev.Anastasis
{
    public class HolySpiritAbility : Ability
    {
        public readonly struct HolySpiritUnitData
        {
            public readonly FloatAttribute AngularSpeed { get; }
            public readonly FloatAttribute CycleDuration { get; }
            public readonly FloatAttribute MinDistance { get; }
            public readonly FloatAttribute MaxDistance { get; }

            public HolySpiritUnitData(FloatAttribute angularSpeed, FloatAttribute cycleDuration, FloatAttribute minDistance, FloatAttribute maxDistance)
            {
                AngularSpeed = angularSpeed;
                CycleDuration = cycleDuration;
                MinDistance = minDistance;
                MaxDistance = maxDistance;
            }
        }

        private readonly HolySpiritAvatar _avatarPrefab;
        [ShowInInspector] private readonly HolySpiritLevel[] _levels;

        private readonly float _minDistanceMultiplier;

        private readonly IntegerAttribute _unitsAmount;
        private readonly List<HolySpiritUnitData> _unitsData;
        private readonly IntegerAttribute _damage;
        private readonly FloatAttribute _cooldown;
        private readonly FloatAttribute _damageMultiplier;

        private HolySpiritAvatar _avatar;

        private Dictionary<Collider, DamageOverTime> _blessingCharacters;

        public override int MaxLevel => _levels.Length - 1;

        public IntegerAttribute UnitsAmount => _unitsAmount;
        public List<HolySpiritUnitData> UnitsData => _unitsData;

        public HolySpiritAbility(CharacterAbilities owner, HolySpiritAbilityConfig config) : base(owner, config)
        {
            _avatarPrefab = config.HolySpiritAvatar;
            _levels = config.Levels;

            _blessingCharacters = new Dictionary<Collider, DamageOverTime>();

            _damageMultiplier = owner.PlayerAttributes.DamageMultiplier;
            _minDistanceMultiplier = config.MinDistanceMultiplier;

            _unitsAmount = new IntegerAttribute(_levels[0].Units.Length);
            _unitsData = new List<HolySpiritUnitData>();

            _damage = new IntegerAttribute(Mathf.CeilToInt(_levels[0].Damage * _damageMultiplier.Value));
            _cooldown = new FloatAttribute(1f); // SI alguien permanece cerca dele spiritu debería poder seguir recibiendo daño. 
            _damageMultiplier.Subscribe(OnDamageMultiplierChange);
        }

        private void OnDamageMultiplierChange(float diff)
        {
            _damage.Value = Mathf.CeilToInt(_levels[_currentLevel].Damage * _damageMultiplier.Value);
        }

        protected override void ApplyUpgrade()
        {
            _damage.Value = Mathf.CeilToInt(_levels[_currentLevel].Damage * _damageMultiplier.Value);

            for (int i = 0; i < _levels[_currentLevel].Units.Length; ++i)
            {
                if (i < _unitsData.Count)
                {
                    _unitsData[i].AngularSpeed.Value = _levels[_currentLevel].Units[i].RotateAngularSpeed;
                    _unitsData[i].CycleDuration.Value = _levels[_currentLevel].Units[i].CycleDuration;
                    _unitsData[i].MaxDistance.Value = _levels[_currentLevel].Units[i].MaxDistance;
                    _unitsData[i].MinDistance.Value = _unitsData[i].MaxDistance.Value * _minDistanceMultiplier;
                    continue;
                }

                var angularSpeed = new FloatAttribute(_levels[_currentLevel].Units[i].RotateAngularSpeed);
                var cycleDuration = new FloatAttribute(_levels[_currentLevel].Units[i].CycleDuration);
                var maxDistance = new FloatAttribute(_levels[_currentLevel].Units[i].MaxDistance);
                var minDistance = new FloatAttribute(maxDistance.Value * _minDistanceMultiplier);

                _unitsData.Add(new HolySpiritUnitData(angularSpeed, cycleDuration, maxDistance, minDistance));
            }

            _unitsAmount.Value = _unitsData.Count;

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

        internal void TryAddTarget(Transform spirit, Collider other)
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

            Assert.IsFalse(_blessingCharacters.ContainsKey(other));

            var damageOverTime = new DamageOverTime(damageReceiver, _owner.Transform, spirit, _owner.TeamId, _damage, _cooldown);

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
