using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    [CreateAssetMenu(menuName = "MiguelGameDev/Anastasis/Compassion", fileName = "CompassionAbility")]
    public class CompassionAbilityConfig : AbilityConfig
    {
        [SerializeField] ParticleSystem _compassionAvatar;

        public ParticleSystem CompassionAvatar => _compassionAvatar;
    }
}
