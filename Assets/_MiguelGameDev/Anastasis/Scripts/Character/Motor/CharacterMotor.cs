using Sirenix.OdinInspector;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{

    public class CharacterMotor
    {
        [ShowInInspector] private Transform _transform;
        [ShowInInspector] private CharacterController _characterController;
        [ShowInInspector] private FloatAttribute _speed;

        [ShowInInspector] private Vector3 _velocity;
        [ShowInInspector] private bool _enable;
        public Vector3 Velocity => _velocity;

        public float SpeedSqr => _velocity.sqrMagnitude;

        public CharacterMotor(CharacterController characterController, FloatAttribute speed)
        {
            _transform = characterController.transform;
            _characterController = characterController;
            _speed = speed;
        }

        public void Init()
        {
            _enable = true;
        }

        public void SetVelocity(Vector2 velocity)
        {
            if (!_enable)
            {
                return;
            }

            _velocity = velocity.ToVector3XZ().normalized * _speed.Value;
        }

        internal async void Push(Vector2 pushForce)
        {
            _enable = false;
            //await 
            _enable = true;
        }

        internal void Stop()
        {
            _enable = false;
            _velocity = Vector3.zero;
        }

        internal void Update()
        {
            if (!_enable)
            {
                return;
            }

            if (_velocity.sqrMagnitude == 0)
            {
                return;
            }

            _transform.LookAt(_transform.position + _velocity);
            _characterController.SimpleMove(_velocity);
        }

    }
}