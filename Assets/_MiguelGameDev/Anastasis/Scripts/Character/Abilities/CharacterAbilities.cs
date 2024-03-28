using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace MiguelGameDev.Anastasis
{

    public class CharacterAbilities
    {
        private readonly Transform _transform;
        private readonly int _teamId;
        private readonly AbilityFactory _factory;
        private readonly ActivateCrownOfThornsUseCase _activateCrownOfThornsUseCase;

        [ShowInInspector] private Dictionary<int, Ability> _abilities;

        public Transform Transform => _transform;
        public int TeamId => _teamId;
        public ActivateCrownOfThornsUseCase ActivateCrownOfThornsUseCase => _activateCrownOfThornsUseCase;

        public CharacterAbilities(Transform transform, int teamId, AbilityFactory factory, ActivateCrownOfThornsUseCase activateCrownOfThornsUseCase)
        {
            _transform = transform;
            _teamId = teamId;
            _factory = factory;
            _activateCrownOfThornsUseCase = activateCrownOfThornsUseCase;
            _abilities = new Dictionary<int, Ability>();
        }

        public CharacterAbilities(Transform transform, int teamId, AbilityFactory factory, AbilityConfig[] defaultAbilities, ActivateCrownOfThornsUseCase activateCrownOfThornsUseCase)
        {
            _transform = transform;
            _teamId = teamId;
            _factory = factory;
            _activateCrownOfThornsUseCase = activateCrownOfThornsUseCase;
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
                var ability = _factory.CreateAbility(this, abilityConfig);
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
                if (ability.CurrentLevel == 0)
                {
                    continue;
                }

                ability.Update();
            }
        }
    }
}
