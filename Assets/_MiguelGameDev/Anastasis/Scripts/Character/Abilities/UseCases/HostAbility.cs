using Sirenix.OdinInspector;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class HostAbility : Ability
    {
        [ShowInInspector] private readonly HostLevel[] _levels;
        private readonly ParticleSystem _hostAvatarPrefab;
        public override int MaxLevel => _levels.Length - 1;

        public HostAbility(CharacterAbilities owner, HostAbilityConfig config) : base(owner, config)
        {
            _levels = config.Levels;
            _hostAvatarPrefab = config.HostAvatar;
        }

        protected override void ApplyUpgrade()
        {
            _owner.PlayerAttributes.DamageMultiplier.Value = _levels[_currentLevel].DamageMultiplier;

            Object.Instantiate(_hostAvatarPrefab, _owner.Transform);
        }

        public override bool Update()
        {
            return false;
        }
    }
}
