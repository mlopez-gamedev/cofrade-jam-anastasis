using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiguelGameDev.Anastasis
{
    public class EndGameUseCase
    {
        private readonly PlayerFacade _player;
        private readonly EnemyWavesSpawner _enemySpawner;
        public ScreensMediator _screensMediator;
        private readonly AudioService _audio;

        public EndGameUseCase(PlayerFacade player, EnemyWavesSpawner enemyWavesSpawner, ScreensMediator screensMediator, AudioService audio)
        {
            _player = player;
            _enemySpawner = enemyWavesSpawner;
            _screensMediator = screensMediator;
            _audio = audio;
        }

        public async UniTask EndGame(bool win)
        {
            _player.Stop();
            _enemySpawner.Stop();

            await UniTask.WaitForSeconds(1.5f);

            if (win)
            {
                await Win();
            }
            else
            {
                await Lose();
            }

            SceneManager.LoadScene(0);
        }

        private UniTask Win()
        {
            _audio.PlayMusic(_audio.WinMusicClip);
            return _screensMediator.ShowStory("Story.Win");
        }

        private UniTask Lose()
        {
            _audio.PlayMusic(_audio.LoseMusicClip);
            return _screensMediator.ShowStory("Story.Lose");
        }
    }
}