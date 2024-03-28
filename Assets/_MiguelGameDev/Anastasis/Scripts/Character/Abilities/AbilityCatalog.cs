using System.Collections.Generic;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    [CreateAssetMenu(menuName = "MiguelGameDev/Anastasis/Ability Catalog", fileName = "AbilityCatalog")]
    public class AbilityCatalog : ScriptableObject
    {
        [SerializeField] private AbilityConfig[] _abilities;

        public AbilityConfig[] Abilities => _abilities;

        public List<AbilityConfig> GetAvailableAbilitiesFor(CharacterAbilities characterAbilities)
        {
            var availableAbilities = new List<AbilityConfig>();
            foreach (AbilityConfig ability in _abilities)
            {
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
                if (ability.Type == EAbilityType.Buff)
                {
                    availableAbilities.Add(ability);
                    continue;
                }
            }

            return availableAbilities;

        }
    }
}
