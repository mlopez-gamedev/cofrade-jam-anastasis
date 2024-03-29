using Cysharp.Threading.Tasks;

namespace MiguelGameDev.Anastasis
{

    public class PlayerDieUseCase : CharacterDieUseCase
    {
        private GameDirector _gameDirector;

        public PlayerDieUseCase(GameDirector gameDirector, CharacterMotor motor, CharacterAnimation animation, CharacterAudio audio, CharacterUi characterUi) : base(motor, animation, audio, characterUi)
        {
            _gameDirector = gameDirector;
        }

        public override async UniTask<bool> CharacterDie()
        {
            if (!await base.CharacterDie())
            {
                // character not died, instead it'll resurrect
                return false;
            }

            _gameDirector.PlayerDie();

            return true;
        }
    }
}