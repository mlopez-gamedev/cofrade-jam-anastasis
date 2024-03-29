using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class EnemyFacade : CharacterFacade
    {
        [SerializeField] private ParticleSystem _dieEffect;

        public void Setup(int teamId, CharacterAttributes attributes, int experience, Transform playerTransform, Camera camera, PlayerKillEnemyUseCase playerKillEnemyUseCase)
        {
            _attributes = attributes;

            _characterUi.Setup(camera, _attributes.MaxHealth, _attributes.CurrentHealth);

            _motor = new CharacterMotor(_characterController, _attributes.Speed);
            _health = new CharacterHealth(_attributes.MaxHealth, _attributes.CurrentHealth);

            var moveUseCase = new MoveCharacterUseCase(_motor, _animation);

            _input = new AiCharacterInput(transform, playerTransform, moveUseCase);

            var dieUseCase = new EnemyDieUseCase(this, _characterUi, _dieEffect, playerKillEnemyUseCase, experience);
            var takeDamageUseCase = new TakeDamageUseCase(_health, _motor, _input, _animation, _audio, _hurtEffect, dieUseCase);

            _damageReceiver.Setup(teamId, attributes.InvulnerabilityDuration, takeDamageUseCase);
            _damager.Setup(transform, teamId, attributes.TouchDamage);
        }

        public UniTask Explode()
        {
            _animation.gameObject.SetActive(false);
            _dieEffect.gameObject.SetActive(true);

            return UniTask.WaitForSeconds(2f);
        }
    }
}
