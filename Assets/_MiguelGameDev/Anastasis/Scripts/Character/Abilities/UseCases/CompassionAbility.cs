using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class CompassionAbility : Ability
    {
        private readonly ParticleSystem _compassionPrefab;
        public override int MaxLevel => int.MaxValue;

        public CompassionAbility(CharacterAbilities owner, CompassionAbilityConfig config) : base(owner, config)
        {
            _compassionPrefab = config.CompassionAvatar;
        }

        protected override void ApplyUpgrade()
        {
            _owner.PlayerAttributes.CurrentHealth.Value = _owner.PlayerAttributes.MaxHealth.Value;

            Object.Instantiate(_compassionPrefab, _owner.Transform);
        }

        public override bool Update()
        {
            return false;
        }
    }
}
