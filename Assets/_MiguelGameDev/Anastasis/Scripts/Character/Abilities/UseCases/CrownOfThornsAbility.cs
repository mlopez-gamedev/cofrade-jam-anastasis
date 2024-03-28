using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class CrownOfThornsAbility : Ability
    {
        private CrownOfThornsLevel[] _levels;
        public override int MaxLevel => _levels.Length - 1;

        private FloatAttribute _minDamageMultiplier;
        private FloatAttribute _maxDamageMultiplier;
        private FloatAttribute _pushForce;
        private FloatAttribute _stuntDuration;

        public CrownOfThornsAbility(CharacterAbilities owner, CrownOfThornsAbilityConfig config) : base(owner, config)
        {
            _levels = config.Levels;

            _minDamageMultiplier = new FloatAttribute(_levels[0].MinDamageMultiplier);
            _maxDamageMultiplier = new FloatAttribute(_levels[0].MaxDamageMultiplier);
        }

        protected override void ApplyUpgrade()
        {
            _minDamageMultiplier.Value = _levels[_currentLevel].MinDamageMultiplier;
            _maxDamageMultiplier.Value = _levels[_currentLevel].MaxDamageMultiplier;

            if (_currentLevel == 1)
            {
                _owner.ActivateCrownOfThornsUseCase.ActivateCrownOfThorns(this);
            }
        }

        public override bool Update()
        {
            return false;
        }

        internal void ReturnDamage(DamageInfo damageInfo)
        {
            var characterDamageReceiver = damageInfo.Instigator.GetComponent<CharacterDamageReceiver>();
            if (characterDamageReceiver == null)
            {
                return;
            }

            var damageMultiplier = Random.Range(_minDamageMultiplier.Value, _maxDamageMultiplier.Value);
            var damage = Mathf.CeilToInt(damageInfo.Damage * damageMultiplier);
            var pushForce = CalcPushForce(damageInfo.Instigator);

            characterDamageReceiver.TakeDamage(new DamageInfo(_owner.Transform, damage, _stuntDuration.Value, pushForce));
        }

        private Vector2 CalcPushForce(Transform instigator)
        {
            if (_pushForce.Value == 0)
            {
                return Vector2.zero;
            }

            var pushDirection = (instigator.position - _owner.Transform.position).normalized;
            return pushDirection * _pushForce.Value;
        }
    }
}
