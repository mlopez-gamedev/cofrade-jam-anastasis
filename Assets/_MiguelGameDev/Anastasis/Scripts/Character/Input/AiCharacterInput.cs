namespace MiguelGameDev.Anastasis
{
    public class AiCharacterInput : CharacterInput
    {
        private readonly MoveCharacterUseCase _moveCharacterUseCase;

        public AiCharacterInput(MoveCharacterUseCase moveCharacterUseCase)
        {
            _moveCharacterUseCase = moveCharacterUseCase;
        }

        public override void Init()
        {
            _enable = true;
        }

        public override void Update()
        {
            if (!_enable)
            {
                return;
            }
        }
    }
}