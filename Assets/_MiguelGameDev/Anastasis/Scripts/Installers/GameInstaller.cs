using Sirenix.OdinInspector;
using System.Threading.Tasks;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class GameInstaller : MonoBehaviour
    {
        [SerializeField, BoxGroup("UI")] private TitleScreen _titleScreen;
        [SerializeField, BoxGroup("UI")] private StoryScreen _storyScreen;
        [SerializeField, BoxGroup("Camera")] private CameraPositioner _cameraPositioner;
        [SerializeField, BoxGroup("Camera")] private Vector3 _titlePosition = new Vector3(0, 0, -14f);
        [SerializeField, BoxGroup("Camera")] private Vector3 _gamePosition = new Vector3(0, 0, -14f);
        [SerializeField, BoxGroup("Camera")] private FollowerCamera _followerCamera;
        [SerializeField, BoxGroup("Camera")] private FollowerCameraSettings _followerCameraSettings;
        [SerializeField, BoxGroup("Game")] private PlayerFacade _player;

        // Start is called before the first frame update
        void Start()
        {
            Install();
        }

        private Task Install()
        {
            var initGameUseCase = new InitGameUseCase(_titleScreen, _storyScreen, _cameraPositioner, _followerCamera, _player);

            _cameraPositioner.Setup(_titlePosition, _gamePosition);
            _followerCamera.Setup(_followerCameraSettings);

            _titleScreen.Setup(initGameUseCase);

            Init();
            return Task.CompletedTask;
        }


        private void Init()
        {
            _cameraPositioner.SetAsTitlePosition();
            _titleScreen.Init();
        }
    }
}