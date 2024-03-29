using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class AiCharacterInput : CharacterInput
    {
        private const float MAX_DISTANCE_SQR = 20f * 20f;
        private readonly Transform _characterTransform;
        private readonly Transform _playerTransform;
        private readonly MoveCharacterUseCase _moveCharacterUseCase;

        private bool _waitingForPlayer;

        public AiCharacterInput(Transform characterTransform, Transform playerTransform, MoveCharacterUseCase moveCharacterUseCase)
        {
            _characterTransform = characterTransform;
            _playerTransform = playerTransform;
            _moveCharacterUseCase = moveCharacterUseCase;
        }

        public override void Init()
        {
            _enable = true;
            _waitingForPlayer = true;
        }

        public override void Update()
        {
            if (!_enable)
            {
                return;
            }

            var playerDirection = _playerTransform.position - _characterTransform.position;
            bool isInPlayerRange = playerDirection.sqrMagnitude < MAX_DISTANCE_SQR;

            if (_waitingForPlayer)
            {
                if (!isInPlayerRange)
                {
                    return;
                }

                _waitingForPlayer = false;
            }

            if (!isInPlayerRange)
            {
                // Teleport near player
                if (EnemyWavesSpawner.GetSpawnPosition(_playerTransform, out Vector3 position))
                {
                    _moveCharacterUseCase.Teleport(position);
                }
                return;
            }

            _moveCharacterUseCase.Move(playerDirection);
        }
    }
}