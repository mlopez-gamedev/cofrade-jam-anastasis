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

        private IntegerAttribute _enemiesKilled;
        private IntegerAttribute _bossesKilled;

        private bool _isGameAlive;

        public bool IsGameAlive => _isGameAlive;

        public GameDirector(PlayerGoals playerGoals, IntegerAttribute enemiesKilled, IntegerAttribute bossesKilled, EndGameUseCase endGameUseCase)
        {
            _playerGoals = playerGoals;
            _endGameUseCase = endGameUseCase;
            _enemiesKilled = enemiesKilled;
            _bossesKilled = bossesKilled;
        }

        internal void Setup(PlayerFacade playerFacade, EnemySpawner[] bossSpawners, EnemyWavesSpawner enemyWavesSpawner)
        {
            _playerFacade = playerFacade;
            _bossSpawners = bossSpawners;
            _enemyWavesSpawner = enemyWavesSpawner;
        }

        internal void Init()
        {
            _enemiesKilled.Value = 0;

            SpawnBoss();
            _enemyWavesSpawner.Init();
            _isGameAlive = true;
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
            if (!_isGameAlive)
            {
                return;
            }

            _isGameAlive = false;
            _endGameUseCase.EndGame(result).Forget();
        }

        public void SpawnBoss()
        {
            var enemy = _bossSpawners[_currentBossIndex].Spawn();
            _playerGoals.Target = enemy;
        }

        internal bool PlayerKillEnemy(EnemyFacade enemyFacade)
        {
            ++_enemiesKilled.Value;
            CheckGameDifficult();
            if (_playerGoals.Target == enemyFacade)
            {
                ++_bossesKilled.Value;
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
