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
            var playerGoals = new PlayerGoals();

            var playerAttributes = new PlayerAttributes(
                    new FloatAttribute(_playerSettings.BaseSpeed),
                    new IntegerAttribute(_playerSettings.BaseMaxHealth),
                    new IntegerAttribute(_playerSettings.BaseMaxHealth),
                    new FloatAttribute(_playerSettings.BaseDamageMultiplier),
                    new FloatAttribute(_playerSettings.BaseInvulnerabilityDuration));

            var abilityFactory = new AbilityFactory();

            var pickAbilityUseCase = new PlayerPickAbilityUseCase(_screensMediator, _abilityCatalog);
            var playerLevelUpUseCase = new PlayerLevelUpUseCase(pickAbilityUseCase);

            _player.Setup(JESUS_TEAM_ID, playerAttributes, abilityFactory, playerLevelUpUseCase);

            var initGameUseCase = new InitGameUseCase(_screensMediator, _cameraPositioner, _followerCamera, _player, _audio, pickAbilityUseCase);

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