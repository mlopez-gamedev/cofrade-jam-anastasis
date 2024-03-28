using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class GameInstaller : MonoBehaviour
    {
        private const int JESUS_TEAM_ID = 0;
        private const int DEMONS_TEAM_ID = 1;

        [SerializeField, BoxGroup("UI")] private TitleScreen _titleScreen;
        [SerializeField, BoxGroup("UI")] private StoryScreen _storyScreen;
        [SerializeField, BoxGroup("UI")] private TutorialScreen _tutorialScreen;
        [SerializeField, BoxGroup("UI")] private PickAbilityScreen _pickAbilityScreen;
        [SerializeField, BoxGroup("Camera")] private CameraPositioner _cameraPositioner;
        [SerializeField, BoxGroup("Camera")] private Vector3 _titlePosition = new Vector3(0, 0, -14f);
        [SerializeField, BoxGroup("Camera")] private Vector3 _gamePosition = new Vector3(0, 0, -14f);
        [SerializeField, BoxGroup("Camera")] private FollowerCamera _followerCamera;
        [SerializeField, BoxGroup("Camera")] private FollowerCameraSettings _followerCameraSettings;
        [SerializeField, BoxGroup("Player")] private PlayerFacade _player;
        [SerializeField, BoxGroup("Player")] private CharacterSettings _playerSettings;
        [SerializeField, BoxGroup("Player")] private AbilityCatalog _abilityCatalog;
        [SerializeField, BoxGroup("Environment")] private Transform _terrain;

        // Start is called before the first frame update
        void Start()
        {
            Install();
        }

        private UniTask Install()
        {
            var playerAttributes = new PlayerAttributes(
                    new FloatAttribute(_playerSettings.BaseSpeed),
                    new IntegerAttribute(_playerSettings.BaseMaxHealth),
                    new IntegerAttribute(_playerSettings.BaseMaxHealth),
                    new FloatAttribute(_playerSettings.BaseDamageMultiplier),
                    new FloatAttribute(_playerSettings.BaseInvulnerabilityDuration));

            var abilityFactory = new AbilityFactory();

            var pickAbilityUseCase = new PlayerPickAbilityUseCase(_pickAbilityScreen, _abilityCatalog);
            var playerLevelUpUseCase = new PlayerLevelUpUseCase(pickAbilityUseCase);

            _player.Setup(JESUS_TEAM_ID, playerAttributes, abilityFactory, playerLevelUpUseCase);

            var initGameUseCase = new InitGameUseCase(_titleScreen, _storyScreen, _tutorialScreen, _cameraPositioner, _followerCamera, _player, pickAbilityUseCase);

            _cameraPositioner.Setup(_titlePosition, _gamePosition);
            _followerCamera.Setup(_followerCameraSettings);

            _titleScreen.Setup(initGameUseCase);

            Init();
            return UniTask.CompletedTask;
        }


        private void Init()
        {
            _cameraPositioner.SetAsTitlePosition();
            _titleScreen.Init();
        }
    }
}