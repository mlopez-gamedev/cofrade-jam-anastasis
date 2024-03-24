using Cysharp.Threading.Tasks;

namespace MiguelGameDev.Anastasis
{
    public class PlayerDieUseCase : CharacterDieUseCase
    {
        public PlayerDieUseCase(CharacterMotor motor, CharacterAnimation animation, CharacterAudio audio) : base(motor, animation, audio)
        {
        }

        public override async UniTask<bool> CharacterDie()
        {
            if (!await base.CharacterDie())
            {
                // character not died, instead it'll resurrect
                return false;
            }

            // end game

            return true;
        }
    }
}