using UnityEngine;

namespace MiguelGameDev.Anastasis
{

    [CreateAssetMenu(menuName = "MiguelGameDev/Anastasis/Holy Spirit", fileName = "HolySpiritAbility")]
    public class HolySpiritAbilityConfig : AbilityConfig
    {
        [SerializeField] HolySpiritAvatar _holySpiritAvatar;
        [SerializeField] float _minDistanceMultiplier = 0.3f;
        [SerializeField] HolySpiritLevel[] _levels;

        public HolySpiritAvatar HolySpiritAvatar => _holySpiritAvatar;
        public float MinDistanceMultiplier => _minDistanceMultiplier;
        public HolySpiritLevel[] Levels => _levels;
    }
}
