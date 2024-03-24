namespace MiguelGameDev.Anastasis
{
    public class InitGameUseCase
    {
        private readonly TitleScreen _titleScreen;
        private readonly StoryScreen _storyScreen;
        private readonly CameraPositioner _camera;
        private readonly FollowerCamera _followerCamera;
        private readonly PlayerFacade _player;

        public InitGameUseCase(TitleScreen titleScreen, StoryScreen storyScreen, CameraPositioner camera, FollowerCamera followerCamera, PlayerFacade player)
        {
            _titleScreen = titleScreen;
            _storyScreen = storyScreen;
            _camera = camera;
            _followerCamera = followerCamera;
            _player = player;
        }

        public async void InitGame()
        {
            await _titleScreen.Hide();
            await _storyScreen.ShowStory("Story.Intro");
            _followerCamera.SetTarget(_player.transform, false, true);
            await _camera.MoveToGamePosition(10f);
            _followerCamera.SetMove(true);
            // Show tutorial panel
            _player.Init();
            // Start game
        }
    }
}