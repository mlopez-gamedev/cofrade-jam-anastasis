using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace MiguelGameDev.Anastasis
{

    public class CharacterAbilities
    {
        private AbilityCatalog _catalog;
        private Dictionary<int, Ability> _abilities;
        private readonly AbilityFactory _factory;

        public void Setup(AbilityCatalog catalog)
        {
            _catalog = catalog;
        }

        public CharacterAbilities(AbilityFactory factory)
        {
            _factory = factory;
            _abilities = new Dictionary<int, Ability>();
        }

        public CharacterAbilities(AbilityFactory factory, AbilityConfig[] defaultAbilities)
        {
            _factory = factory;
            _abilities = new Dictionary<int, Ability>(defaultAbilities.Length);
            foreach (var ability in defaultAbilities)
            {
                AddAbility(ability);
            }
        }

        public bool HasAbility(int abilityId)
        {
            return _abilities.ContainsKey(abilityId);
        }

        public Ability GetAbility(int abilityId)
        {
            Assert.IsTrue(HasAbility(abilityId), "Character has not ability");
            return _abilities[abilityId];
        }

        public bool CanAddAbility(int abilityId)
        {
            if (!_abilities.ContainsKey(abilityId))
            {
                return true;
            }

            return _abilities[abilityId].CanUpgrade;
        }

        public bool AddAbility(AbilityConfig abilityConfig)
        {
            bool isNew = false;
            int abilityId = abilityConfig.Key.GetHashCode();
            if (!HasAbility(abilityId))
            {
                var ability = _factory.CreateAbility(abilityConfig);
                Assert.IsNotNull(ability, $"Ability {abilityConfig.Key} not found");
                _abilities.Add(abilityId, ability);
                isNew = true;
            }


            _abilities[abilityId].Upgrade();

            return isNew;
        }



        public void Update()
        {
            foreach (Ability ability in _abilities.Values)
            {
                ability.TryExecute();
            }
        }
    }
}
