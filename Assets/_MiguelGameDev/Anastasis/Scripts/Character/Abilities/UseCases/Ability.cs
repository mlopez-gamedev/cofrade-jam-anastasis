using Sirenix.OdinInspector;
using UnityEngine.Assertions;
using static UnityEngine.UI.GridLayoutGroup;

namespace MiguelGameDev.Anastasis
{
    public abstract class Ability
    {
        protected readonly CharacterAbilities _owner;

        [ShowInInspector] protected int _currentLevel;

        public int CurrentLevel => _currentLevel;
        public abstract int MaxLevel { get; }
        public bool CanUpgrade => _currentLevel < MaxLevel;

        public Ability(CharacterAbilities owner, AbilityConfig config)
        {
            _owner = owner;
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