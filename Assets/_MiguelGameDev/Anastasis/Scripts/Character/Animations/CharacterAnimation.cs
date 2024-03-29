using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class CharacterAnimation : MonoBehaviour
    {
        [SerializeField] protected Animator _animator;

        private int _isSleepingId;
        private int _speedSqrId;
        private int _hurtId;
        private int _isDeadId;
        private int _attackId;
        private int _attackSpeedMultiplier;

        private AnimationClip _attackClip;

        private void Awake()
        {
            _isSleepingId = Animator.StringToHash("isSleeping");
            _speedSqrId = Animator.StringToHash("speedSqr");
            _hurtId = Animator.StringToHash("hurt");
            _isDeadId = Animator.StringToHash("isDead");
            _attackId = Animator.StringToHash("attack");
            _attackSpeedMultiplier = Animator.StringToHash("attackSpeedMultiplier");

            SetClips();
        }

        private void SetClips()
        {
            foreach (var clip in _animator.runtimeAnimatorController.animationClips)
            {
                var animationName = clip.name.Split('@')[1];
                if (animationName == "attack")
                {
                    _attackClip = clip;
                }
            }
        }

        public virtual void WakeUp()
        {
            _animator.SetBool(_isSleepingId, false);
        }

        public void SetSpeedSqr(float speedSqr)
        {
            _animator.SetFloat(_speedSqrId, speedSqr);
        }

        public void TriggerHurt()
        {
            _animator.SetTrigger(_hurtId);
        }

        public virtual void SetIsDead(bool isDead)
        {
            _animator.SetBool(_isDeadId, isDead);
        }

        public UniTask TriggerAttack(float maxDuration)
        {
            _animator.SetTrigger(_attackId);
            float duration = SetAttackSpeed(maxDuration);
            return UniTask.Delay((int)((duration + 0.02f) * 1000));
        }

        private float SetAttackSpeed(float maxDuration)
        {
            var speedMultiplier = 1f;
            var animationLength = _attackClip.length;
            if (animationLength > maxDuration)
            {
                speedMultiplier = animationLength / maxDuration;
            }
            _animator.SetFloat(_attackSpeedMultiplier, speedMultiplier);
            return _attackClip.length * speedMultiplier;
        }

        internal void Stop()
        {
            _animator.SetFloat(_speedSqrId, 0);
        }
    }
}