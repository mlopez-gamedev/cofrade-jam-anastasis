using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class PlayerPickAbilityUseCase
    {
        public const int ABILITIES_AMOUNT = 3;

        private readonly PickAbilityScreen _pickAbilityScreen;
        private readonly CharacterAbilities _characterAbilities;
        private readonly AbilityCatalog _abilityCatalog;

        public PlayerPickAbilityUseCase(PickAbilityScreen pickAbilityScreen, CharacterAbilities characterAbilities, AbilityCatalog abilityCatalog) 
        {
            _pickAbilityScreen = pickAbilityScreen;
            _characterAbilities = characterAbilities;
            _abilityCatalog = abilityCatalog;
        }

        public UniTask PickInitialAbility()
        {
            var availableAbilities = _abilityCatalog.GetInitialAbilities();
            return PickAbility(availableAbilities);
        }

        public UniTask PickRandomAbility()
        {
            var availableAbilities = _abilityCatalog.GetInitialAbilities();
            return PickAbility(availableAbilities);
        }

        private async UniTask PickAbility(List<AbilityConfig> availableAbilities)
        {
            var abilities = GetRandomAbilities(ABILITIES_AMOUNT, availableAbilities);
            var ability = await _pickAbilityScreen.PickAbility(_characterAbilities, abilities);
            if (_characterAbilities.AddAbility(ability))
            {
                // is new;
            }
            // is a upgrade
        }

        private AbilityConfig[] GetRandomAbilities(int maxAmount, List<AbilityConfig> availableAbilities)
        {
            var amount = Mathf.Min(maxAmount, availableAbilities.Count);
            var abilities = new AbilityConfig[amount];
            for (int i = 0; i < amount; ++i)
            {
                var ability = availableAbilities[Random.Range(0, availableAbilities.Count)];
                abilities[i] = ability;
                availableAbilities.Remove(ability);
            }
            return abilities;
        }
    }
}