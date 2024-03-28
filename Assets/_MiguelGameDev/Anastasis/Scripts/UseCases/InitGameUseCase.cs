using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{

    public class InitGameUseCase
    {
        private readonly TitleScreen _titleScreen;
        private readonly StoryScreen _storyScreen;
        private readonly TutorialScreen _tutorialScreen;
        private readonly CameraPositioner _camera;
        private readonly FollowerCamera _followerCamera;
        private readonly PlayerFacade _player;
        private readonly AudioService _audio;
        private readonly PlayerPickAbilityUseCase _pickAbilityUseCase;

        public InitGameUseCase(TitleScreen titleScreen, StoryScreen storyScreen, TutorialScreen tutorialScreen, 
                CameraPositioner camera, FollowerCamera followerCamera, PlayerFacade player, AudioService audio, PlayerPickAbilityUseCase pickAbilityUseCase)
        {
            _titleScreen = titleScreen;
            _storyScreen = storyScreen;
            _tutorialScreen = tutorialScreen;
            _camera = camera;
            _followerCamera = followerCamera;
            _player = player;
            _audio = audio;
            _pickAbilityUseCase = pickAbilityUseCase;
        }

        public async void InitGame()
        {
            await _titleScreen.Hide();
            await _storyScreen.ShowStory("Story.Intro");
            await ZoomOutCamera();
            _player.WakeUp();
            _audio.PlayAmbient();
            await _audio.StopMusic(1f);
            await _tutorialScreen.ShowTutorial();
            await _pickAbilityUseCase.PickInitialAbility(_player.Abilities);
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
        }
    }
}