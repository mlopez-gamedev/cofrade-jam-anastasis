using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class PlayerLevelUpUseCase
    {
        private readonly GameDirector _gameDirector;
        private readonly CharacterAbilities _characterAbilities;
        private readonly PlayerPickAbilityUseCase _playerPickAbilityUseCase;

        public PlayerLevelUpUseCase(GameDirector gameDirector, CharacterAbilities characterAbilities, PlayerPickAbilityUseCase playerPickAbilityUseCase)
        {
            _gameDirector = gameDirector;
            _characterAbilities = characterAbilities;
            _playerPickAbilityUseCase = playerPickAbilityUseCase;
        }

        public async UniTask LevelUp()
        {
            await UniTask.WaitForSeconds(0.6f); //Wait so yo can see die the enemy
            if (!_gameDirector.IsGameAlive)
            {
                return;
            }
            await _playerPickAbilityUseCase.PickRandomAbility(_characterAbilities);
        }
    }
}