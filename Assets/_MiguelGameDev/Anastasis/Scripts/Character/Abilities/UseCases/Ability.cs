using UnityEngine.Assertions;

namespace MiguelGameDev.Anastasis
{

    public abstract class Ability
    {
        protected int _maxLevel;
        protected int _currentLevel;

        public int MaxLevel => _maxLevel;
        public int CurrentLevel => _currentLevel;
        public bool CanUpgrade => _currentLevel < _maxLevel;

        public Ability(AbilityConfig config)
        {
            _maxLevel = config.MaxLevel;
            _currentLevel = 0;
        }

        public void Upgrade()
        {
            Assert.IsTrue(CanUpgrade, "You are triying upgrade, but max level reached");
            ++_currentLevel;
            ApplyUpgrade();
        }

        protected abstract void ApplyUpgrade();

        public abstract bool TryExecute();
    }
}
