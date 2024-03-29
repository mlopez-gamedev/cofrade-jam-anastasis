using Sirenix.OdinInspector;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{

    public class PlayerFacade : CharacterFacade
    {
        [ShowInInspector, HideInEditorMode, BoxGroup("Game State")] protected CharacterAbilities _abilities;
        [ShowInInspector, HideInEditorMode, BoxGroup("Game State")] protected PlayerExperience _experience;

        [SerializeField] CrownOfThornsAvatar _crownOfThrons;

        public CharacterAbilities Abilities => _abilities;
        public PlayerExperience Experience => _experience;

        public void Setup(int teamId, CharacterAttributes attributes, AbilityFactory abilityFactory, PlayerPickAbilityUseCase playerPickAbilityUseCase)
        {
            var activateCrownOfThornsUseCase = new ActivateCrownOfThornsUseCase(_crownOfThrons);

            _attributes = attributes;

            _motor = new CharacterMotor(_characterController, _attributes.Speed);
            _health = new CharacterHealth(_attributes.MaxHealth, _attributes.CurrentHealth);
            _abilities = new CharacterAbilities(transform, teamId, attributes, abilityFactory, activateCrownOfThornsUseCase);

            var playerLevelUpUseCase = new PlayerLevelUpUseCase(_abilities, playerPickAbilityUseCase);
            _experience = new PlayerExperience(playerLevelUpUseCase);

            var moveUseCase = new MoveCharacterUseCase(_motor, _animation);

            _input = new UnityLegacyCharacterInput(moveUseCase);

            var dieUseCase = new PlayerDieUseCase(_motor, _animation, _audio);
            var takeDamageUseCase = new TakeDamageUseCase(_health, _motor, _input, _animation, _audio, dieUseCase, _crownOfThrons);

            _damageReceiver.Setup(teamId, attributes.InvulnerabilityDuration, takeDamageUseCase);
        }

        protected override void Update()
        {
            base.Update();
            _abilities.Update();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _abilities?.Release();
        }
    }
}