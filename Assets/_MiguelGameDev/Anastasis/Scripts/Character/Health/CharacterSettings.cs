using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    [CreateAssetMenu(menuName = "MiguelGameDev/Anastasis/Character Settings", fileName = "CharacterSettings")]
    public class CharacterSettings : ScriptableObject
    {
        [SerializeField] private float _baseSpeed;
        [SerializeField] private int _baseMaxHealth;
        [SerializeField] private float _baseInvulnerabilityDuration;
        [SerializeField] private float _baseDamageMultiplier = 1f;

        public float BaseSpeed => _baseSpeed;
        public int BaseMaxHealth => _baseMaxHealth;
        public float BaseInvulnerabilityDuration => _baseInvulnerabilityDuration;
        public float BaseDamageMultiplier => _baseDamageMultiplier;

    }
}