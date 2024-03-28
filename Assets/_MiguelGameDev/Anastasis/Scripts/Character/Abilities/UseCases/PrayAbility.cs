using Sirenix.OdinInspector;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class PrayAbility : Ability
    {
        [ShowInInspector] private readonly PrayLevel[] _levels;

        private PrayAvatar _prayAvatarPrefab;

        private FloatAttribute _healMultiplier;
        private FloatAttribute _cooldown;
        private IntegerAttribute _heal;
        private PrayAvatar _prayAvatar;

        private float _nextPrayTime;

        public override int MaxLevel => _levels.Length - 1;

        public PrayAbility(CharacterAbilities owner, PrayAbilityConfig config) : base(owner, config)
        {
            _levels = config.Levels;
            _prayAvatarPrefab = config.PrayAvatar;

            _healMultiplier = owner.PlayerAttributes.DamageMultiplier; // De momento uso el mismo multiplicador que el daño

            _cooldown = new FloatAttribute(_levels[0].Cooldown);
            _heal = new IntegerAttribute(Mathf.CeilToInt(_levels[0].Heal * _healMultiplier.Value));
            _healMultiplier.Subscribe(OnHealMultiplierChange);
        }

        private void OnHealMultiplierChange(float diff)
        {
            _heal.Value = Mathf.CeilToInt(_levels[_currentLevel].Heal * _healMultiplier.Value);
        }

        protected override void ApplyUpgrade()
        {
            _cooldown.Value = _levels[_currentLevel].Cooldown;
            _heal.Value = Mathf.CeilToInt(_levels[_currentLevel].Heal * _healMultiplier.Value);

            if (_currentLevel == 1)
            {
                _prayAvatar = Object.Instantiate(_prayAvatarPrefab, _owner.Transform);
            }

            _nextPrayTime = Time.time + _cooldown.Value;
        }

        public override bool Update()
        {
            if (Time.time < _nextPrayTime)
            {
                return false;
            }

            var newHealth = Mathf.Min(_owner.PlayerAttributes.MaxHealth.Value, _owner.PlayerAttributes.CurrentHealth.Value + _heal.Value);
            if (newHealth == _owner.PlayerAttributes.CurrentHealth.Value)
            {
                return false;
            }

            _owner.PlayerAttributes.CurrentHealth.Value = newHealth;

            _prayAvatar.Play();
            _nextPrayTime = Time.time + _cooldown.Value;
            return true;
        }

        internal override void Release()
        {
            _healMultiplier.Unsubscribe(OnHealMultiplierChange);
        }
    }
}
