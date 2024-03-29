using System.Collections.Generic;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    [CreateAssetMenu(menuName = "MiguelGameDev/Anastasis/Enemies Catalog", fileName = "EnemiesCatalog")]
    public class EnemyCatalog : ScriptableObject
    {
        [SerializeField] private EnemySettings[] _enemies;

        public EnemySettings GetEnemyByKey(string key)
        {
            foreach (var enemy in _enemies)
            {
                if (enemy.Key == key)
                {
                    return enemy;
                }
            }

            return null;
        }

        public EnemySettings GetRandomEnemy(int level)
        {
            var enemiesChances = new Dictionary<EnemySettings, float>();
            float totalChances = 0;
            foreach (var enemy in _enemies)
            {
                if (level < enemy.MinLevel)
                {
                    continue;
                }

                enemiesChances.Add(enemy, enemy.Chances);
                totalChances += enemy.Chances;
            }

            float randomChances = Random.Range(0, totalChances);
            foreach (var enemy in enemiesChances)
            {
                totalChances -= enemy.Value;
                if (randomChances >= totalChances)
                {
                    return enemy.Key;
                }
            }

            return null;
        }
    }
}