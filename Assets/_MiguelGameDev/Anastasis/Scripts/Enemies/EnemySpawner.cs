using UnityEngine;

namespace MiguelGameDev.Anastasis
{

    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemySettings _enemy;
        [SerializeField] private int _level;

        private EnemiesFactory _enemiesFactory;

        public void Setup(EnemiesFactory enemiesFactory)
        {
            _enemiesFactory = enemiesFactory;
        }

        public EnemyFacade Spawn()
        {
            var enemy = _enemiesFactory.CreateEnemy(_enemy, _level, transform.position);
            enemy.WakeUp();
            enemy.Init();
            return enemy;
        }
    }
}