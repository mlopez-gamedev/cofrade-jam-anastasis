using Sirenix.OdinInspector;
using System;
using UnityEngine.Assertions;
using static UnityEngine.UI.GridLayoutGroup;

namespace MiguelGameDev.Anastasis
{
    public abstract class Ability
    {
        protected readonly CharacterAbilities _owner;
        protected readonly EAbilityType _type;

        [ShowInInspector] protected int _currentLevel;

        public int CurrentLevel => _currentLevel;
        public abstract int MaxLevel { get; }
        public bool CanUpgrade => _type == EAbilityType.FullHealing || _currentLevel < MaxLevel;

        public Ability(CharacterAbilities owner, AbilityConfig config)
        {
            _owner = owner;
            _type = config.Type;
            _currentLevel = 0;
        }

        [Button]
        public void Upgrade()
        {
            Assert.IsTrue(CanUpgrade, "You are triying upgrade, but max level reached");
            ++_currentLevel;
            ApplyUpgrade();
        }

        protected abstract void ApplyUpgrade();

        public abstract bool Update();
        internal virtual void Release()
        {

        }
    }
}