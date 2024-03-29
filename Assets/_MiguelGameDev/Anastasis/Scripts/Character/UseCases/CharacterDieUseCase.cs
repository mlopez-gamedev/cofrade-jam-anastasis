using Cysharp.Threading.Tasks;

namespace MiguelGameDev.Anastasis
{
    public class CharacterDieUseCase
    {
        private readonly CharacterMotor _motor;
        protected readonly CharacterAnimation _animation;
        private readonly CharacterAudio _audio;
        private readonly CharacterUi _characterUi;

        public CharacterDieUseCase(CharacterMotor motor, CharacterAnimation animation, CharacterAudio audio, CharacterUi characterUi)
        {
            _motor = motor;
            _audio = audio;
            _characterUi = characterUi;
            _animation = animation;
        }

        public virtual async UniTask<bool> CharacterDie()
        {
            _characterUi.gameObject.SetActive(false);
            _motor.Stop();
            _animation.SetIsDead(true);
            _audio.PlayDieAudio();

            // TODO: resurrect ?
            return true;
        }
    }
}