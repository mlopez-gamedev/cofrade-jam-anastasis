using MiguelGameDev.Anastasis;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerFacade : MonoBehaviour
{
    [SerializeField] CrownOfThornsAvatar _crownOfThrons;

    [SerializeField] CharacterController _characterController;
    [SerializeField] CharacterAnimation _animation;
    [SerializeField] CharacterAudio _audio;
    [SerializeField] CharacterDamageReceiver _damageReceiver;

    [ShowInInspector, HideInEditorMode, BoxGroup("Game State")] CharacterAbilities _abilities;
    [ShowInInspector, HideInEditorMode, BoxGroup("Game State")] private CharacterMotor _motor;
    [ShowInInspector, HideInEditorMode, BoxGroup("Game State")] private CharacterHealth _health;
    private UnityLegacyCharacterInput _input;

    public CharacterAbilities Abilities => _abilities;

    public void Setup(int teamId, PlayerAttributes playerAttributes, AbilityFactory abilityFactory)
    {
        var activateCrownOfThornsUseCase = new ActivateCrownOfThornsUseCase(_crownOfThrons);

        _motor = new CharacterMotor(_characterController, playerAttributes.Speed);
        _health = new CharacterHealth(playerAttributes.MaxHealth, playerAttributes.CurrentHealth);
        _abilities = new CharacterAbilities(transform, teamId, abilityFactory, activateCrownOfThornsUseCase);

        var moveUseCase = new MoveCharacterUseCase(_motor, _animation);
        
        _input = new UnityLegacyCharacterInput(moveUseCase);

        var dieUseCase = new PlayerDieUseCase(_motor, _animation, _audio);
        var takeDamageUseCase = new TakeDamageUseCase(_health, _motor, _input, _animation, _audio, dieUseCase, _crownOfThrons);

        _damageReceiver.Setup(teamId, playerAttributes.InvulnerabilityDuration, takeDamageUseCase);
    }

    public void WakeUp()
    {
        _animation.WakeUp();
    }

    public void Init()
    {
        _input.Init();
        _motor.Init();
    }

    private void Update()
    {
        _input.Update();
        _motor.Update();
        _abilities.Update();
    }

    public void Pause()
    {

    }

    public void Resume()
    {

    }
}
