using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class PlayerLevelUpUseCase
    {
        private readonly CharacterAbilities _characterAbilities;
        private readonly PlayerPickAbilityUseCase _playerPickAbilityUseCase;

        public PlayerLevelUpUseCase(CharacterAbilities characterAbilities, PlayerPickAbilityUseCase playerPickAbilityUseCase)
        {
            _characterAbilities = characterAbilities;
            _playerPickAbilityUseCase = playerPickAbilityUseCase;
        }

        public void LevelUp()
        {
            _playerPickAbilityUseCase.PickRandomAbility(_characterAbilities).Forget();
        }
    }
}