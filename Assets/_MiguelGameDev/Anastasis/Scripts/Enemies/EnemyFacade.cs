using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class EnemyFacade : CharacterFacade
    {
        public virtual void Setup(int teamId, CharacterAttributes attributes, AbilityFactory abilityFactory)
        {
            _attributes = attributes;

            _motor = new CharacterMotor(_characterController, _attributes.Speed);
            _health = new CharacterHealth(_attributes.MaxHealth, _attributes.CurrentHealth);

            var moveUseCase = new MoveCharacterUseCase(_motor, _animation);

            _input = new AiCharacterInput(moveUseCase);

            var dieUseCase = new PlayerDieUseCase(_motor, _animation, _audio);
            var takeDamageUseCase = new TakeDamageUseCase(_health, _motor, _input, _animation, _audio, dieUseCase);

            _damageReceiver.Setup(teamId, attributes.InvulnerabilityDuration, takeDamageUseCase);
        }
    }
}
