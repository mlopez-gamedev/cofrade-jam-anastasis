using Cysharp.Threading.Tasks;

namespace MiguelGameDev.Anastasis
{
    public class GameDirector
    {
        private int BOSS_LEVEL_GROWTH = 3;

        private readonly PlayerGoals _playerGoals;
        private readonly EndGameUseCase _endGameUseCase;
        private PlayerFacade _playerFacade;
        private EnemySpawner[] _bossSpawners;
        private EnemyWavesSpawner _enemyWavesSpawner;

        private int _currentPlayerLevel;
        private int _currentBossIndex;
        private int _enemiesKilled;

        private bool _gameAlive;

        public GameDirector(PlayerGoals playerGoals, EndGameUseCase endGameUseCase)
        {
            _playerGoals = playerGoals;
            _endGameUseCase = endGameUseCase;
        }

        internal void Setup(PlayerFacade playerFacade, EnemySpawner[] bossSpawners, EnemyWavesSpawner enemyWavesSpawner)
        {
            _playerFacade = playerFacade;
            _bossSpawners = bossSpawners;
            _enemyWavesSpawner = enemyWavesSpawner;
        }

        internal void Init()
        {
            _enemiesKilled = 0;

            SpawnBoss();
            _enemyWavesSpawner.Init();
            _gameAlive = true;
        }

        public void PlayerDie()
        {
            EndGame(false);
        }

        public void PlayerWin()
        {
            EndGame(true);
        }

        private void EndGame(bool result)
        {
            if (!_gameAlive)
            {
                return;
            }

            _gameAlive = false;
            _endGameUseCase.EndGame(result).Forget();
        }

        public void SpawnBoss()
        {
            var enemy = _bossSpawners[_currentBossIndex].Spawn();
            _playerGoals.Target = enemy;
        }

        internal bool PlayerKillEnemy(EnemyFacade enemyFacade)
        {
            ++_enemiesKilled;
            CheckGameDifficult();
            if (_playerGoals.Target == enemyFacade)
            {
                if (!NextBoss())
                {
                    return false;
                }
                _enemyWavesSpawner.ChangeLevel(_currentPlayerLevel + _currentBossIndex * BOSS_LEVEL_GROWTH);
            }
            else
            {
                _enemyWavesSpawner.SubstractEnemy(enemyFacade);
            }

            return true;
        }


        private bool NextBoss()
        {
            _currentBossIndex++;

            if (_currentBossIndex < _bossSpawners.Length)
            {
                SpawnBoss();
                return true;
            }

            PlayerWin();
            return false;
        }

        private void CheckGameDifficult()
        {
            if (_playerFacade.Experience.CurrentLevel == _currentPlayerLevel)
            {
                return;
            }

            _currentPlayerLevel = _playerFacade.Experience.CurrentLevel;

            _enemyWavesSpawner.ChangeLevel(_currentPlayerLevel + _currentBossIndex * BOSS_LEVEL_GROWTH);
        }
    }
}
