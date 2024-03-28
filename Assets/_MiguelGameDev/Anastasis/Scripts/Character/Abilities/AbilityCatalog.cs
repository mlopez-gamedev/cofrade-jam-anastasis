using System.Collections.Generic;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{

    [CreateAssetMenu(menuName = "MiguelGameDev/Anastasis/Ability Catalog", fileName = "AbilityCatalog")]
    public class AbilityCatalog : ScriptableObject
    {
        [SerializeField] private AbilityConfig[] _abilities;

        public AbilityConfig[] Abilities => _abilities;

        public List<AbilityConfig> GetAvailableAbilitiesFor(CharacterAbilities characterAbilities, out AbilityConfig fullHealingAbility)
        {
            fullHealingAbility = null;
            var availableAbilities = new List<AbilityConfig>();
            foreach (AbilityConfig ability in _abilities)
            {
                if (ability.Type == EAbilityType.FullHealing)
                {
                    if (characterAbilities.PlayerAttributes.CurrentHealth.Value < characterAbilities.PlayerAttributes.MaxHealth.Value)
                    {
                        fullHealingAbility = ability;
                    }
                    continue;
                }

                if (characterAbilities.CanAddAbility(ability.Id))
                {
                    availableAbilities.Add(ability);
                }
            }

            return availableAbilities;
        }

        public List<AbilityConfig> GetInitialAbilities()
        {
            // only attack abilities as you starts empty
            var availableAbilities = new List<AbilityConfig>();
            foreach (AbilityConfig ability in _abilities)
            {
                if (ability.Type == EAbilityType.Attack)
                {
                    availableAbilities.Add(ability);
                    continue;
                }
            }

            return availableAbilities;

        }
    }
}
