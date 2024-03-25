
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class EnemyAttackUseCase
    {
        private readonly CharacterMotor _motor;
        private readonly CharacterInput _input;
        private readonly CharacterAnimation _animator;

        public EnemyAttackUseCase(CharacterMotor motor, CharacterInput input, CharacterAnimation animator)
        {
            _motor = motor;
            _input = input;
            _animator = animator;
        }

        public async UniTask Attack(Attack attack)
        {
            _input.SetEnable(false);
            _motor.Stop();
            await _animator.TriggerAttack(attack.Duration);
            _input.SetEnable(true);
            _motor.Stop();
        }
    }
}
