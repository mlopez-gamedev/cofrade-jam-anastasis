using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{

    public class EnemyWavesSpawner : MonoBehaviour
    {
        private const float MIN_DISTANCE = 15f;
        private const float MAX_DISTANCE = 20f;

        private Transform _playerTransform;
        private EnemiesFactory _enemiesFactory;

        [ShowInInspector, HideInEditorMode] private float _spawnCooldown;
        [ShowInInspector, HideInEditorMode] private int _maxEnemyAmount;
        [ShowInInspector, HideInEditorMode] private int _difficultLevel = 1;

        [ShowInInspector, HideInEditorMode] private List<EnemyFacade> _aliveEnemies;
        [ShowInInspector, HideInEditorMode] private float _nextCooldownTime;

        private FloatLinearInterpolator _spawnCooldownInterpolator;
        private IntegerLinearInterpolator _maxEnemyAmountInterpolator;

        public void Setup(Transform playerTransform, EnemiesFactory enemiesFactory)
        {
            _playerTransform = playerTransform;
            _enemiesFactory = enemiesFactory;

            _spawnCooldownInterpolator = new FloatLinearInterpolator(1, 110, 0.15f, 2f);
            _maxEnemyAmountInterpolator = new IntegerLinearInterpolator(1, 110, 5, 60);
        }

        public void Init()
        {
            _aliveEnemies = new List<EnemyFacade>();
            _difficultLevel = 1;
            SetLevelValues();
            _nextCooldownTime = Time.time + _spawnCooldown;
        }

        public void ChangeLevel(int level)
        {
            _difficultLevel = level;
            SetLevelValues();
        }

        private void Update()
        {
            if (_nextCooldownTime == 0)
            {
                return;
            }

            if (Time.time < _nextCooldownTime)
            {
                return;
            }

            if (_aliveEnemies.Count >= _maxEnemyAmount)
            {
                return;
            }

            Spawn();
        }

        public void Spawn()
        {
            if (GetSpawnPosition(out Vector3 position))
            {
                var enemy = _enemiesFactory.CreateRandomEnemy(_difficultLevel, position);
                enemy.WakeUp();
                enemy.Init();
                _aliveEnemies.Add(enemy);

                _nextCooldownTime = Time.time + _spawnCooldown;
            }
        }

        private bool GetSpawnPosition(out Vector3 position)
        {
            var playerPosition = _playerTransform.position;
            bool isValid;
            int maxTries = 3;
            do
            {
                position = GetRandomPosition(playerPosition);
                isValid = IsValidPosition(position);
                if (!isValid)
                {
                    --maxTries;
                }
            }
            while (!isValid && maxTries > 0);

            return isValid;
        }

        public static Vector3 GetRandomPosition(Vector3 playerPosition)
        {
            var distance = Random.Range(MIN_DISTANCE, MAX_DISTANCE);
            var offset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * distance;
            return playerPosition + offset;
        }

        private bool IsValidPosition(Vector3 position)
        {
            if (position.x < -480f || position.x > 480f)
            {
                return false;
            }

            if (position.y < -480f || position.y > 480f)
            {
                return false;
            }

            return true;
        }

        public void SubstractEnemy(EnemyFacade enemy)
        {
            _aliveEnemies.Remove(enemy);
        }

        public void Stop()
        {
            _nextCooldownTime = 0;
            foreach (var enemy in _aliveEnemies)
            {
                enemy.Stop();
            }
        }

        private void SetLevelValues()
        {
            _maxEnemyAmount = _maxEnemyAmountInterpolator.GetValue(_difficultLevel);
            _spawnCooldown = _spawnCooldownInterpolator.GetInverseValue(_difficultLevel);
        }
    }
}