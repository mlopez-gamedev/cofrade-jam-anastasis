using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    [CreateAssetMenu(menuName = "MiguelGameDev/Anastasis/Enemy Settings", fileName = "EnemySettings")]
    public class EnemySettings : CharacterSettings
    {
        [SerializeField] private int _minLevel;
        [SerializeField] private float _chances;
        [SerializeField] private int _baseExperience;
        [SerializeField] private EnemyFacade _prefab;

        public string Key => name;
        public int MinLevel => _minLevel;
        public float Chances => _chances;
        public int BaseExperience => _baseExperience;
        public EnemyFacade Prefab => _prefab;

    }
}