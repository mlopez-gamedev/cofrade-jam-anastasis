using MiguelGameDev.Anastasis;
using UnityEngine;

public class PlayerFacade : MonoBehaviour
{
    [SerializeField] CharacterController _characterController;
    [SerializeField] CharacterAnimation _animation;
    [SerializeField] CharacterAudio _audio;
    [SerializeField] CharacterDamageReceiver _damageReceiver;
    [SerializeField] CharacterAbilities _abilities;

    private CharacterMotor _motor;
    private CharacterHealth _health;
    private UnityLegacyCharacterInput _input;

    public CharacterAbilities Abilities => _abilities;

    public void Setup(PlayerAttributes playerAttributes, AbilityFactory abilityFactory)
    {

        _motor = new CharacterMotor(_characterController, playerAttributes.Speed);
        _health = new CharacterHealth(playerAttributes.MaxHealth, playerAttributes.CurrentHealth);
        _abilities = new CharacterAbilities(abilityFactory);

        var moveUseCase = new MoveCharacterUseCase(_motor, _animation);
        
        _input = new UnityLegacyCharacterInput(moveUseCase);

        var dieUseCase = new PlayerDieUseCase(_motor, _animation, _audio);
        var takeDamageUseCase = new TakeDamageUseCase(_health, _motor, _input, _animation, _audio, dieUseCase);

        _damageReceiver.Setup(playerAttributes.InvulnerabilityDuration, takeDamageUseCase);
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
    }

    public void Pause()
    {

    }

    public void Resume()
    {

    }
}
