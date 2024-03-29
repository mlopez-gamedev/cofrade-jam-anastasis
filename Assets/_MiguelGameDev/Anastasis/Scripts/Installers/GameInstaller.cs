using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class GameInstaller : MonoBehaviour
    {
        private const int JESUS_TEAM_ID = 0;
        private const int DEMONS_TEAM_ID = 1;

        [SerializeField, BoxGroup("Audio")] private AudioService _audio;
        [SerializeField, BoxGroup("Ui")] private ScreensMediator _screensMediator;
        [SerializeField, BoxGroup("Environment")] private Transform _terrain;
        [SerializeField, BoxGroup("Camera")] private Camera _camera;
        [SerializeField, BoxGroup("Camera")] private CameraPositioner _cameraPositioner;
        [SerializeField, BoxGroup("Camera")] private Vector3 _titlePosition = new Vector3(0, 0, -14f);
        [SerializeField, BoxGroup("Camera")] private Vector3 _gamePosition = new Vector3(0, 0, -14f);
        [SerializeField, BoxGroup("Camera")] private FollowerCamera _followerCamera;
        [SerializeField, BoxGroup("Camera")] private FollowerCameraSettings _followerCameraSettings;
        [SerializeField, BoxGroup("Player")] private PlayerFacade _player;
        [SerializeField, BoxGroup("Player")] private CharacterSettings _playerSettings;
        [SerializeField, BoxGroup("Player")] private AbilityCatalog _abilityCatalog;
        [SerializeField, BoxGroup("Enemies")] private EnemyCatalog _enemyCatalog;
        [SerializeField, BoxGroup("Enemies")] private EnemyWavesSpawner _enemyWavesSpawner;
        [SerializeField, BoxGroup("Enemies")] private EnemySpawner[] _bossSpawners;

        // Start is called before the first frame update
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;

            Install();
        }

        private UniTask Install()
        {
            var playerGoals = new PlayerGoals();

            var playerAttributes = new CharacterAttributes(
                    new FloatAttribute(_playerSettings.BaseSpeed),
                    new IntegerAttribute(_playerSettings.BaseMaxHealth),
                    new IntegerAttribute(_playerSettings.BaseMaxHealth),
                    new IntegerAttribute(_playerSettings.BaseTouchDamage),
                    new FloatAttribute(_playerSettings.BaseDamageMultiplier),
                    new FloatAttribute(_playerSettings.BaseInvulnerabilityDuration));

           
            var endGameUseCase = new EndGameUseCase(_player, _enemyWavesSpawner, _screensMediator, _audio);
            var gameDirector = new GameDirector(playerGoals, endGameUseCase);

            // Abilities
            var pickAbilityUseCase = new PlayerPickAbilityUseCase(_screensMediator, _abilityCatalog);
            var abilityFactory = new AbilityFactory();
            var activateCrownOfThornsUseCase = new ActivateCrownOfThornsUseCase(_player.CrownOfThrons);
            var abilities = new CharacterAbilities(_player.transform, JESUS_TEAM_ID, playerAttributes, abilityFactory, activateCrownOfThornsUseCase);

            // Experience
            var experienceAttributes = new ExperienceAttributes(new IntegerAttribute(1), new IntegerAttribute(0), new IntegerAttribute(0));
            var playerLevelUpUseCase = new PlayerLevelUpUseCase(abilities, pickAbilityUseCase);
            var experience = new PlayerExperience(playerLevelUpUseCase, experienceAttributes.CurrentLevel, experienceAttributes.CurrentExperience, experienceAttributes.NextLevelExperience);

            // Player
            _player.Setup(gameDirector, JESUS_TEAM_ID, playerAttributes, experience, abilities, _camera, pickAbilityUseCase);

            // Enemies
            var playerKillsEnemyUseCase = new PlayerKillEnemyUseCase(_player.Experience, gameDirector);
            var enemiesFactory = new EnemiesFactory(_enemyCatalog, DEMONS_TEAM_ID, _player.transform, _camera, playerKillsEnemyUseCase);

            foreach (var bossSpawner in _bossSpawners)
            {
                bossSpawner.Setup(enemiesFactory);
            }
            _enemyWavesSpawner.Setup(_player.transform, enemiesFactory);

            gameDirector.Setup(_player, _bossSpawners, _enemyWavesSpawner);

            var initGameUseCase = new InitGameUseCase(_screensMediator, _cameraPositioner, _followerCamera, _player, _audio, gameDirector, pickAbilityUseCase);

            _cameraPositioner.Setup(_titlePosition, _gamePosition);
            _followerCamera.Setup(_followerCameraSettings);

            _screensMediator.Setup(_audio, _player.transform, playerGoals, initGameUseCase);

            Init();
            return UniTask.CompletedTask;
        }


        private void Init()
        {
            _cameraPositioner.SetAsTitlePosition();
            _screensMediator.Init();
        }
    }
}