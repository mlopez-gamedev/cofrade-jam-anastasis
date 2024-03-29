using Sirenix.OdinInspector;
using System;

namespace MiguelGameDev.Anastasis
{
    public class PlayerExperience
    {
        int _baseExperienceNeeded;
        int _experienceIncrement;

        private readonly PlayerLevelUpUseCase _playerLevelUpUseCase;
        private int _currentLevel;
        private int _nextLevelExperience;
        private int _currentExperience;

        public PlayerExperience(PlayerLevelUpUseCase playerLevelUpUseCase)
        {
            _playerLevelUpUseCase = playerLevelUpUseCase;
        }

        public void AddExperience(int experience)
        {
            _currentExperience += experience;
            CheckLevelUp();
        }

        private void CheckLevelUp()
        {
            if (_currentExperience < _nextLevelExperience)
            {
                return;
            }

            LevelUp();
        }

        [Button]
        private void LevelUp()
        {
            ++_currentLevel;
            UpdateNextLevelExperience();

            _playerLevelUpUseCase.LevelUp();
        }

        private void UpdateNextLevelExperience()
        {
            
        }
    }
}