using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class ScreensMediator : MonoBehaviour
    {
        [SerializeField, BoxGroup("UI")] private TitleScreen _titleScreen;
        [SerializeField, BoxGroup("UI")] private StoryScreen _storyScreen;
        [SerializeField, BoxGroup("UI")] private TutorialScreen _tutorialScreen;
        [SerializeField, BoxGroup("UI")] private PickAbilityScreen _pickAbilityScreen;
        [SerializeField, BoxGroup("UI")] private GameScreen _gameScreen;

        public void Setup(AudioService audio, Transform player, IntegerAttribute playerLevel, IntegerAttribute enemiesKilled, IntegerAttribute bossesKilled, IntegerAttribute totalBosses,
             PlayerGoals playerGoals, InitGameUseCase initGameUseCase)
        {
            _titleScreen.Setup(audio, initGameUseCase);
            _storyScreen.Setup(audio);
            _tutorialScreen.Setup(audio);
            _pickAbilityScreen.Setup(audio);
            _gameScreen.Setup(player, playerGoals, playerLevel, enemiesKilled, bossesKilled, totalBosses);
        }

        internal void Init()
        {
            _titleScreen.Init();
        }

        internal UniTask HideTitle()
        {
            return _titleScreen.Hide();
        }

        internal UniTask<AbilityConfig> PickAbility(CharacterAbilities characterAbilities, AbilityConfig[] abilityConfigs)
        {
            return _pickAbilityScreen.PickAbility(characterAbilities, abilityConfigs);
        }

        internal UniTask ShowStory(string storyTerm)
        {
            return _storyScreen.ShowStory(storyTerm);
        }

        internal UniTask ShowTutorial()
        {
            return _tutorialScreen.ShowTutorial();
        }

        internal void ShowMap()
        {
            _gameScreen.Show();
        }
    }
}
