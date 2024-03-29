using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class PlayerExperience
    {
        [ShowInInspector] int _maxLevel = 120;
        [ShowInInspector] int _baseExperienceNeeded = 20;
        [ShowInInspector] int _experienceIncrement = 50;
        [ShowInInspector] int _power = 2;

        [ShowInInspector] private readonly PlayerLevelUpUseCase _playerLevelUpUseCase;
        [ShowInInspector] private readonly IntegerAttribute _currentLevel;
        [ShowInInspector] private readonly IntegerAttribute _currentExperience;
        [ShowInInspector] private readonly IntegerAttribute _nextLevelExperience;

        [ShowInInspector] public int CurrentLevel => _currentLevel.Value;

        public PlayerExperience(PlayerLevelUpUseCase playerLevelUpUseCase, IntegerAttribute currentLevel, IntegerAttribute currentExperience, IntegerAttribute nextLevelExperience)
        {
            _playerLevelUpUseCase = playerLevelUpUseCase;
            _currentLevel = currentLevel;
            _currentExperience = currentExperience;
            _nextLevelExperience = nextLevelExperience;
        }

        public void Init()
        {
            _currentLevel.Value = 1;
            UpdateNextLevelExperience();
        }

        public void AddExperience(int experience)
        {
            _currentExperience.Value += experience;
        }

        public UniTask CheckLevelUp()
        {
            if (_currentLevel.Value == _maxLevel)
            {
                return UniTask.CompletedTask;
            }

            if (_currentExperience.Value < _nextLevelExperience.Value)
            {
                return UniTask.CompletedTask;
            }

            return LevelUp();
        }

        [Button]
        private UniTask LevelUp()
        {
            ++_currentLevel.Value;
            UpdateNextLevelExperience();

            return _playerLevelUpUseCase.LevelUp();
        }

        private void UpdateNextLevelExperience()
        {
            if (_currentLevel.Value == _maxLevel)
            {
                return;
            }
            _nextLevelExperience.Value = _baseExperienceNeeded + _experienceIncrement * (int)Mathf.Pow(_currentLevel.Value, _power);
        }
    }
}