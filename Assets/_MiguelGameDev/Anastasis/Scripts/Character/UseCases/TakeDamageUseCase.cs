using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class TakeDamageUseCase
    {
        private readonly CharacterHealth _health;
        private readonly CharacterMotor _motor;
        private readonly CharacterInput _characterInput;
        private readonly CharacterAnimation _animation;
        private readonly CharacterAudio _audio;
        private readonly CharacterDieUseCase _dieUseCase;

        private readonly IDamageEffector _damageEffector;

        public TakeDamageUseCase(CharacterHealth health, CharacterMotor motor, CharacterInput characterInput, CharacterAnimation animation, 
                CharacterAudio audio, CharacterDieUseCase dieUseCase, IDamageEffector damageEffector = null)
        {
            _health = health;
            _motor = motor;
            _characterInput = characterInput;
            _animation = animation;
            _audio = audio;
            _damageEffector = damageEffector;
            _dieUseCase = dieUseCase;
        }

        public bool TakeDamage(DamageInfo damageInfo)
        {
            if (!DoDamage(damageInfo.Damage))
            {
                return false;
            }

            _damageEffector.DoDamageEffector(damageInfo);
            Push(damageInfo.PushForce);
            Stunt(damageInfo.StuntDuration);

            return true;
        }

        private bool DoDamage(int damage)
        {
            if (damage <= 0)
            {
                return false;
            }

            // TODO: hurt effect (blood)
            _health.TakeDamage(damage);
            if (_health.IsDead)
            {
                _dieUseCase.CharacterDie();
                return false;
            }

            _animation.TriggerHurt();
            _audio.PlayHurtAudio();
            return true;
        }

        private void Push(Vector2 pushForce)
        {
            if (pushForce.sqrMagnitude == 0)
            {
                return;
            }

            _motor.Push(pushForce);
        }

        private async void Stunt(float duration)
        {
            if (duration <= 0)
            {
                return;
            }

            // Do not accumulate stunts
            if (!_characterInput.Enable)
            {
                return;
            }

            _characterInput.SetEnable(false);
            await UniTask.Delay((int)(duration * 1000));
            _characterInput.SetEnable(true);
        }
    }
}