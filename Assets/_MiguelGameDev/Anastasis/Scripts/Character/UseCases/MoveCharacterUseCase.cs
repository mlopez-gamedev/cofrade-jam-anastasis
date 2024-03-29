using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class MoveCharacterUseCase
    {
        private readonly CharacterMotor _motor;
        private readonly CharacterAnimation _animation;

        public MoveCharacterUseCase(CharacterMotor motor, CharacterAnimation animation)
        {
            _motor = motor;
            _animation = animation;
        }

        public void Move(Vector3 direction)
        {
            float previousSpeedSqr = _motor.SpeedSqr;
            _motor.SetVelocity(direction);

            float currentSpeedSqr = _motor.SpeedSqr;
            if (previousSpeedSqr == currentSpeedSqr)
            {
                return;
            }

            _animation.SetSpeedSqr(currentSpeedSqr);
            // Step sounds?
        }

        public void Move(Vector2 inputVelocity)
        {
            float previousSpeedSqr = _motor.SpeedSqr;
            _motor.SetVelocity(inputVelocity);

            float currentSpeedSqr = _motor.SpeedSqr;
            if (previousSpeedSqr == currentSpeedSqr)
            {
                return;
            }

            _animation.SetSpeedSqr(currentSpeedSqr);
            // Step sounds?
        }
    }
}