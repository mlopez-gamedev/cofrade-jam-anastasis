using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class UnityLegacyCharacterInput : CharacterInput
    {
        private readonly MoveCharacterUseCase _moveUseCase;
        

        public UnityLegacyCharacterInput(MoveCharacterUseCase moveUseCase)
        {
            _moveUseCase = moveUseCase;
        }

        public override void Init()
        {
            _enable = true;
        }

        // Update is called once per frame
        public override void Update()
        {
            if (!_enable)
            {
                return;
            }

            UpdateMovementInput();
        }

        private void UpdateMovementInput()
        {
            var inputVelocity = new Vector2(
                    Input.GetAxis("Horizontal"),
                    Input.GetAxis("Vertical"));

            _moveUseCase.Move(inputVelocity);
        }
    }
}