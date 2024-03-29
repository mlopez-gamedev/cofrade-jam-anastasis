using Sirenix.OdinInspector;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class CharacterFacade : MonoBehaviour
    {
        [SerializeField] protected CharacterController _characterController;
        [SerializeField] protected CharacterUi _characterUi;
        [SerializeField] protected ParticleSystem _hurtEffect;

        [SerializeField] protected CharacterAnimation _animation;
        [SerializeField] protected CharacterAudio _audio;
        [SerializeField] protected Damager _damager;
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
        public CharacterInput Input => _input;
        public CharacterAttributes Attributes => _attributes;

        private void Awake()
        {
            _characterUi.gameObject.SetActive(false);
        }

        public void WakeUp()
        {
            _animation.WakeUp();
        }

        public virtual void Init()
        {
            _characterUi.gameObject.SetActive(true);
            _input.Init();
            _motor.Init();
        }

        protected virtual void Update()
        {
            _input.Update();
            _motor.Update();
        }

        public virtual void Stop()
        {
            _characterUi.gameObject.SetActive(false);
            _input?.SetEnable(false);
            _motor?.Stop();
        }

        private void OnDestroy()
        {
            Stop();
        }
    }
}