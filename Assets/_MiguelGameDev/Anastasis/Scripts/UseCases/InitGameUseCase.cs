using Cysharp.Threading.Tasks;

namespace MiguelGameDev.Anastasis
{

    public class InitGameUseCase
    {
        private readonly ScreensMediator _screensMediator;
        private readonly CameraPositioner _camera;
        private readonly FollowerCamera _followerCamera;
        private readonly PlayerFacade _player;
        private readonly AudioService _audio;
        private readonly GameDirector _gameDirector;
        private readonly PlayerPickAbilityUseCase _pickAbilityUseCase;

        public InitGameUseCase(ScreensMediator screensMediator, CameraPositioner camera, FollowerCamera followerCamera, 
            PlayerFacade player, AudioService audio, GameDirector gameDirector, PlayerPickAbilityUseCase pickAbilityUseCase)
        {
            _screensMediator = screensMediator;
            _camera = camera;
            _followerCamera = followerCamera;
            _player = player;
            _audio = audio;
            _gameDirector = gameDirector;
            _pickAbilityUseCase = pickAbilityUseCase;
        }

        public async void InitGame()
        {
            await _screensMediator.HideTitle();
            await _screensMediator.ShowStory("Story.Intro");
            await ZoomOutCamera();
            _player.WakeUp();
            _audio.PlayAmbient();
            await _audio.StopMusic(1f);
            await _screensMediator.ShowTutorial();
            await _pickAbilityUseCase.PickInitialAbility(_player.Abilities);
            _screensMediator.ShowMap();
            _audio.PlayMusic(_audio.GameMusicClip);
            StartGame();
        }

        private async UniTask ZoomOutCamera()
        {
            _followerCamera.SetTarget(_player.transform, false, true);
            await _camera.MoveToGamePosition(10f);
            _followerCamera.SetMove(true);
        }

        private void StartGame()
        {
            _player.Init();
            _gameDirector.Init();
        }
    }
}