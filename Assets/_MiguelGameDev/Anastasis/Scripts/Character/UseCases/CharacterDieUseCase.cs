using Cysharp.Threading.Tasks;

namespace MiguelGameDev.Anastasis
{
    public class CharacterDieUseCase
    {
        private readonly CharacterMotor _motor;
        private readonly CharacterAnimation _animation;
        private readonly CharacterAudio _audio;

        public CharacterDieUseCase(CharacterMotor motor, CharacterAnimation animation, CharacterAudio audio)
        {
            _motor = motor;
            _audio = audio;
            _animation = animation;
        }

        public virtual async UniTask<bool> CharacterDie()
        {
            _motor.Stop();
            _animation.SetIsDead(true);
            _audio.PlayDieAudio();

            // TODO: resurrect ?
            return true;
        }
    }
}