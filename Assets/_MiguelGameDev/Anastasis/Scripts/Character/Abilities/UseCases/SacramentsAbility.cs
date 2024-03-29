using UnityEngine;
using UnityEngine.Pool;

namespace MiguelGameDev.Anastasis
{
    public class SacramentsAbility : Ability
    {
        private readonly SacramentAvatar _avatarPrefab;
        private SacramentsLevel[] _levels;

        private readonly IntegerAttribute _amount;
        private readonly FloatAttribute _cooldown;
        private readonly FloatAttribute _range;
        private readonly FloatAttribute _duration;
        private readonly IntegerAttribute _damage;
        private readonly FloatAttribute _damageMultiplier;
        private readonly FloatAttribute _stuntDuration;
        private readonly FloatAttribute _pushForce;

        private ObjectPool<SacramentAvatar> _avatarPool;

        private float _nextSacramentTime;

        public override int MaxLevel => _levels.Length - 1;

        public FloatAttribute Duration => _duration;
        public FloatAttribute Range => _range;
        public IntegerAttribute Damage => _damage;

        public SacramentsAbility(CharacterAbilities owner, SacramentsAbilityConfig config) : base(owner, config)
        {
            _avatarPrefab = config.SacramentAvatar;
            _levels = config.Levels;

            _damageMultiplier = owner.PlayerAttributes.DamageMultiplier;

            _amount = new IntegerAttribute(_levels[0].Amount);
            _cooldown = new FloatAttribute(_levels[0].Cooldown);
            _range = new FloatAttribute(_levels[0].Range);
            _duration = new FloatAttribute(_levels[0].Duration);
            _damage = new IntegerAttribute(_levels[0].Damage);
            _pushForce = new FloatAttribute(_levels[0].PushForce);
            _stuntDuration = new FloatAttribute(_levels[0].StuntDuration);

            _avatarPool = new ObjectPool<SacramentAvatar>(CreateAvatar, null, ReturnAvatarToPool);
            _damageMultiplier.Subscribe(OnDamageMultiplierChange);
        }

        private void OnDamageMultiplierChange(float diff)
        {
            _damage.Value = Mathf.CeilToInt(_levels[_currentLevel].Damage * _damageMultiplier.Value);
        }

        private SacramentAvatar CreateAvatar()
        {
            var avatar = Object.Instantiate(_avatarPrefab);
            avatar.gameObject.SetActive(false);
            avatar.Setup(this, _avatarPool);
            return avatar;
        }

        private void ReturnAvatarToPool(SacramentAvatar avatar)
        {
            avatar.Finish();
        }

        protected override void ApplyUpgrade()
        {
            _amount.Value = _levels[_currentLevel].Amount;
            _cooldown.Value = _levels[_currentLevel].Cooldown;
            _range.Value = _levels[_currentLevel].Range;
            _duration.Value = _levels[_currentLevel].Duration;
            _damage.Value = Mathf.CeilToInt(_levels[_currentLevel].Damage * _damageMultiplier.Value);
            _pushForce.Value = _levels[_currentLevel].PushForce;
            _stuntDuration.Value = _levels[_currentLevel].StuntDuration;

            if (_currentLevel == 1)
            {
                _nextSacramentTime = Time.time + _cooldown.Value;
            }
        }

        public override bool Update()
        {
            if (Time.time < _nextSacramentTime)
            {
                return false;
            }

            var shotInfo = GetSacramentsShotInfo(_amount.Value);
            for (int i = 0; i < _amount.Value; ++i)
            {
                var avatar = _avatarPool.Get();
                avatar.Init(_owner.Transform.position, _owner.Transform.rotation, shotInfo[i].Rotation, shotInfo[i].Delay);
            }

            _nextSacramentTime = Time.time + _cooldown.Value;

            return true;
        }

        internal bool TryMakeDamage(Transform sacrament, Collider other)
        {
            CharacterDamageReceiver damageReceiver = other.GetComponent<CharacterDamageReceiver>();
            if (damageReceiver == null)
            {
                return true;
            }

            if (_owner.TeamId == damageReceiver.TeamId)
            {
                return false;
            }

            var pushForce = CalcPushForce(other.transform);
            damageReceiver.TryTakeDamage(new DamageInfo(_owner.Transform, sacrament, _owner.TeamId, _damage.Value, _stuntDuration.Value, pushForce));
            return true;
        }

        public SacramentShotInfo[] GetSacramentsShotInfo(int amount)
        {
            var sacramentsShotInfo = new SacramentShotInfo[amount];
            switch (amount)
            {
                case 1:
                    sacramentsShotInfo[0] = new SacramentShotInfo(0, 0);
                    break;

                case 2:
                    sacramentsShotInfo[0] = new SacramentShotInfo(0, 0);
                    sacramentsShotInfo[1] = new SacramentShotInfo(180f, 0.1f);
                    break;

                case 3:
                    sacramentsShotInfo[0] = new SacramentShotInfo(0, 0);
                    sacramentsShotInfo[1] = new SacramentShotInfo(120f, 0.1f);
                    sacramentsShotInfo[2] = new SacramentShotInfo(240f, 0.2f);
                    break;

                case 4:
                    sacramentsShotInfo[0] = new SacramentShotInfo(0, 0);
                    sacramentsShotInfo[1] = new SacramentShotInfo(90f, 0.1f);
                    sacramentsShotInfo[2] = new SacramentShotInfo(180f, 0.2f);
                    sacramentsShotInfo[3] = new SacramentShotInfo(270f, 0.03f);
                    break;

                case 5:
                    sacramentsShotInfo[0] = new SacramentShotInfo(0, 0);
                    sacramentsShotInfo[2] = new SacramentShotInfo(72f, 0.05f);
                    sacramentsShotInfo[3] = new SacramentShotInfo(144f, 0.1f);
                    sacramentsShotInfo[4] = new SacramentShotInfo(216f, 0.15f);
                    sacramentsShotInfo[5] = new SacramentShotInfo(288f, 0.2f);
                    break;

                case 6:
                case 7:
                    sacramentsShotInfo[0] = new SacramentShotInfo(0, 0);
                    sacramentsShotInfo[1] = new SacramentShotInfo(60f, 0.05f);
                    sacramentsShotInfo[2] = new SacramentShotInfo(120f, 0.1f);
                    sacramentsShotInfo[3] = new SacramentShotInfo(180f, 0.15f);
                    sacramentsShotInfo[4] = new SacramentShotInfo(240f, 0.2f);
                    sacramentsShotInfo[5] = new SacramentShotInfo(300f, 0.25f);
                    break;

                default:
                    sacramentsShotInfo[0] = new SacramentShotInfo(0, 0);
                    sacramentsShotInfo[1] = new SacramentShotInfo(45f, 0.05f);
                    sacramentsShotInfo[2] = new SacramentShotInfo(90f, 0.1f);
                    sacramentsShotInfo[3] = new SacramentShotInfo(135f, 0.15f);
                    sacramentsShotInfo[4] = new SacramentShotInfo(180f, 0.2f);
                    sacramentsShotInfo[5] = new SacramentShotInfo(225f, 0.25f);
                    sacramentsShotInfo[6] = new SacramentShotInfo(270f, 0.3f);
                    sacramentsShotInfo[7] = new SacramentShotInfo(315f, 0.35f);
                    break;
            }

            return sacramentsShotInfo;

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

        internal override void Release()
        {
            _damageMultiplier.Unsubscribe(OnDamageMultiplierChange);
        }
    }
}
