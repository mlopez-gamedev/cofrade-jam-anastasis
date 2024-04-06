using UnityEditor;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class EnemiesFactory
    {
        private float LEVEL_MULTIPLIER_MULT = 0.15f;
        private float LEVEL_MULTIPLIER_POW = 1.5f;
        private readonly EnemyCatalog _catalog;
        private readonly PlayerKillEnemyUseCase _playerKillEnemyUseCase;
        private readonly int _teamId;
        private readonly Transform _playerTransform;
        private readonly Camera _camera;

        public EnemiesFactory(EnemyCatalog catalog, int teamId, Transform playerTransform, Camera camera, PlayerKillEnemyUseCase playerKillEnemyUseCase)
        {
            _catalog = catalog;
            _teamId = teamId;
            _playerTransform = playerTransform;
            _camera = camera;
            _playerKillEnemyUseCase = playerKillEnemyUseCase;
        }

        public EnemyFacade CreateEnemy(EnemySettings enemySettings, int level, Vector3 position)
        {
            var attributes = GenerateAttributes(enemySettings, level);
            var randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            var enemy = Object.Instantiate(enemySettings.Prefab, position, randomRotation);
            enemy.Setup(_teamId, attributes, enemySettings.BaseExperience * level, _playerTransform, _camera, _playerKillEnemyUseCase);

            return enemy;
        }

        public EnemyFacade CreateRandomEnemy(int level, Vector3 position)
        {
            var enemySettings = _catalog.GetRandomEnemy(level);

            var attributes = GenerateAttributes(enemySettings, level);
            var randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            var enemy = Object.Instantiate(enemySettings.Prefab, position, randomRotation);
            enemy.Setup(_teamId, attributes, enemySettings.BaseExperience * level, _playerTransform, _camera, _playerKillEnemyUseCase);

            return enemy;
        }

        private CharacterAttributes GenerateAttributes(EnemySettings settings, int level)
        {
            var multiplier = GetLevelMultiplier(level);

            var health = Mathf.CeilToInt(settings.BaseMaxHealth * multiplier);
            return new CharacterAttributes(
                    new FloatAttribute(settings.BaseSpeed),
                    new IntegerAttribute(health),
                    new IntegerAttribute(health),
                    new IntegerAttribute(settings.BaseTouchDamage),
                    new FloatAttribute(settings.BaseDamageMultiplier * multiplier),
                    new FloatAttribute(settings.BaseInvulnerabilityDuration));
        }

        public float GetLevelMultiplier(int level)
        {
            // Al nivel 110 (máxima subida del player) quiero un multiplicador de x100,
            // pero no quiero que la primera subida de nivel duplique el multiplicador, así que necesito una subida exponencial
            // El 1,28 me da lo que quiero
            return 1 + LEVEL_MULTIPLIER_MULT * Mathf.Pow(level - 1, LEVEL_MULTIPLIER_POW);
        }
    }
}