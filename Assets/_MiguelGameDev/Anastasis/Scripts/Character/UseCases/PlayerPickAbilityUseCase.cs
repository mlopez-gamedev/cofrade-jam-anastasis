using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class PlayerPickAbilityUseCase
    {
        public const int ABILITIES_AMOUNT = 3;

        private readonly PickAbilityScreen _pickAbilityScreen;
        private readonly AbilityCatalog _abilityCatalog;

        public PlayerPickAbilityUseCase(PickAbilityScreen pickAbilityScreen, AbilityCatalog abilityCatalog) 
        {
            _pickAbilityScreen = pickAbilityScreen;
            _abilityCatalog = abilityCatalog;
        }

        public UniTask PickInitialAbility(CharacterAbilities characterAbilities)
        {
            var availableAbilities = _abilityCatalog.GetInitialAbilities();
            return PickAbility(characterAbilities, availableAbilities);
        }

        public UniTask PickRandomAbility(CharacterAbilities characterAbilities)
        {
            var availableAbilities = _abilityCatalog.GetInitialAbilities();
            return PickAbility(characterAbilities, availableAbilities);
        }

        private async UniTask PickAbility(CharacterAbilities characterAbilities, List<AbilityConfig> availableAbilities)
        {
            var abilities = GetRandomAbilities(ABILITIES_AMOUNT, availableAbilities);
            var ability = await _pickAbilityScreen.PickAbility(characterAbilities, abilities);
            if (characterAbilities.AddAbility(ability))
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