using Cysharp.Threading.Tasks;

namespace MiguelGameDev.Anastasis
{
    public class PlayerKillEnemyUseCase {
        private readonly PlayerExperience _playerExperience;
        private readonly GameDirector _gameDirector;

        public PlayerKillEnemyUseCase(PlayerExperience playerExperience, GameDirector gameDirector)
        {
            _playerExperience = playerExperience;
            _gameDirector = gameDirector;
        }

        public UniTask PlayerKillEnemy(EnemyFacade enemyFacade, int experience)
        {
            _playerExperience.AddExperience(experience);
            if (!_gameDirector.PlayerKillEnemy(enemyFacade))
            {
                return UniTask.CompletedTask;
            }
            return _playerExperience.CheckLevelUp();
        }
    }
}