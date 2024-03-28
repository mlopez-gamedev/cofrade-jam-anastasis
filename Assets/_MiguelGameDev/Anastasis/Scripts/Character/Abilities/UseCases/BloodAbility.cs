using Sirenix.OdinInspector;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class BloodAbility : Ability
    {
        [ShowInInspector] private readonly BloodLevel[] _levels;
        private readonly ParticleSystem _bloodAvatarPrefab;
        public override int MaxLevel => _levels.Length - 1;

        public BloodAbility(CharacterAbilities owner, BloodAbilityConfig config) : base(owner, config)
        {
            _levels = config.Levels;
            _bloodAvatarPrefab = config.BloodAvatar;
        }

        protected override void ApplyUpgrade()
        {
            int previousMaxHealth = _owner.PlayerAttributes.MaxHealth.Value;
            _owner.PlayerAttributes.MaxHealth.Value = _levels[_currentLevel].MaxHealth;
            _owner.PlayerAttributes.CurrentHealth.Value += _owner.PlayerAttributes.MaxHealth.Value - previousMaxHealth;

            Object.Instantiate(_bloodAvatarPrefab, _owner.Transform);
        }

        public override bool Update()
        {
            return false;
        }
    }
}
