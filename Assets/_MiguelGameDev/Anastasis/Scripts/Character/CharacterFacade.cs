using Sirenix.OdinInspector;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class CharacterFacade : MonoBehaviour
    {
        [SerializeField] protected CharacterController _characterController;

        [SerializeField] protected CharacterAnimation _animation;
        [SerializeField] protected CharacterAudio _audio;
        [SerializeField] protected CharacterDamageReceiver _damageReceiver;

        [ShowInInspector, HideInEditorMode, BoxGroup("Game State")] protected CharacterMotor _motor;
        [ShowInInspector, HideInEditorMode, BoxGroup("Game State")] protected CharacterHealth _health;
        [ShowInInspector, HideInEditorMode, BoxGroup("Game State")] protected CharacterAttributes _attributes;
        protected CharacterInput _input;

        public CharacterAudio Audio => _audio;
        public CharacterAnimation Animation => _animation;
        public CharacterDamageReceiver DamageReceiver => _damageReceiver;
        public CharacterMotor Motor => _motor;
        public CharacterHealth Health => _health;
        public CharacterAttributes Attributes => _attributes;

        public void WakeUp()
        {
            _animation.WakeUp();
        }

        public void Init()
        {
            _input.Init();
            _motor.Init();
        }

        protected virtual void Update()
        {
            _input.Update();
            _motor.Update();
        }

        protected virtual void OnDestroy()
        {
            _input?.SetEnable(false);
            _motor?.Stop();
        }
    }
}