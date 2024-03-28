using UnityEngine;

namespace MiguelGameDev.Anastasis
{

    [CreateAssetMenu(menuName = "MiguelGameDev/Anastasis/Blood", fileName = "BloodAbility")]
    public class BloodAbilityConfig : AbilityConfig
    {
        [SerializeField] private ParticleSystem _bloodAvatar;
        [SerializeField] private BloodLevel[] _levels;

        public ParticleSystem BloodAvatar => _bloodAvatar;
        public BloodLevel[] Levels => _levels;
    }
}
