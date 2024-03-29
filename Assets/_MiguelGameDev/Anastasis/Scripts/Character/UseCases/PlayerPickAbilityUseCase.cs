using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class PlayerPickAbilityUseCase
    {
        public const int ABILITIES_AMOUNT = 3;

        private readonly ScreensMediator _screensMediator;
        private readonly AbilityCatalog _abilityCatalog;

        public PlayerPickAbilityUseCase(ScreensMediator screensMediator, AbilityCatalog abilityCatalog) 
        {
            _screensMediator = screensMediator;
            _abilityCatalog = abilityCatalog;
        }

        public async UniTask PickInitialAbility(CharacterAbilities characterAbilities)
        {
            Time.timeScale = 0;
            var availableAbilities = _abilityCatalog.GetInitialAbilities();
            await PickAbility(characterAbilities, availableAbilities);
            Time.timeScale = 1f;
        }

        public async UniTask PickRandomAbility(CharacterAbilities characterAbilities)
        {
            Time.timeScale = 0;
            var availableAbilities = _abilityCatalog.GetAvailableAbilitiesFor(characterAbilities, out AbilityConfig fullHealingAbility);
            await PickAbility(characterAbilities, availableAbilities, fullHealingAbility);
            Time.timeScale = 1f;
        }

        private async UniTask PickAbility(CharacterAbilities characterAbilities, List<AbilityConfig> availableAbilities, AbilityConfig fullHealingAbility)
        {
            if (fullHealingAbility == null)
            {
                await PickAbility(characterAbilities, availableAbilities);
                return;
            }

            var abilities = GetRandomAbilities(ABILITIES_AMOUNT - 1, availableAbilities);
            var abilitiesList = new List<AbilityConfig>(abilities)
            {
                fullHealingAbility
            };

            var ability = await _screensMediator.PickAbility(characterAbilities, abilitiesList.ToArray());
            if (characterAbilities.AddAbility(ability))
            {
                // is new;
            }
            // is a upgrade
        }

        private async UniTask PickAbility(CharacterAbilities characterAbilities, List<AbilityConfig> availableAbilities)
        {
            var abilities = GetRandomAbilities(ABILITIES_AMOUNT, availableAbilities);
            var ability = await _screensMediator.PickAbility(characterAbilities, abilities);
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