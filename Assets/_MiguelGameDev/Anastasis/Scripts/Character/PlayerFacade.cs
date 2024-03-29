using Sirenix.OdinInspector;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{

    public class PlayerFacade : CharacterFacade
    {
        [ShowInInspector, HideInEditorMode, BoxGroup("Game State")] protected CharacterAbilities _abilities;
        [ShowInInspector, HideInEditorMode, BoxGroup("Game State")] protected PlayerExperience _experience;

        [SerializeField] CrownOfThornsAvatar _crownOfThrons;

        public CrownOfThornsAvatar CrownOfThrons => _crownOfThrons;

        public CharacterAbilities Abilities => _abilities;
        public PlayerExperience Experience => _experience;

        public void Setup(GameDirector gameDirector, int teamId, CharacterAttributes attributes, PlayerExperience experience,
                CharacterAbilities abilities, Camera camera, PlayerPickAbilityUseCase playerPickAbilityUseCase)
        {
            _attributes = attributes;
            _experience = experience;
            _abilities = abilities;

            _characterUi.Setup(camera, _attributes.MaxHealth, _attributes.CurrentHealth);

            _motor = new CharacterMotor(_characterController, _attributes.Speed);
            _health = new CharacterHealth(_attributes.MaxHealth, _attributes.CurrentHealth);


            var moveUseCase = new MoveCharacterUseCase(_motor, _animation);

            _input = new UnityLegacyCharacterInput(moveUseCase);

            var dieUseCase = new PlayerDieUseCase(gameDirector, _motor, _animation, _audio, _characterUi);
            var takeDamageUseCase = new TakeDamageUseCase(_health, _motor, _input, _animation, _audio, _hurtEffect, dieUseCase, _crownOfThrons);

            _damageReceiver.Setup(teamId, attributes.InvulnerabilityDuration, takeDamageUseCase);
            _damager.Setup(transform, teamId, attributes.TouchDamage);
        }

        public override void Init()
        {
            base.Init();
            _experience.Init();
        }

        protected override void Update()
        {
            base.Update();
            _abilities.Update();
        }

        public override void Stop()
        {
            base.Stop();
            _abilities?.Release();
        }
    }
}