using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    [CreateAssetMenu(menuName = "MiguelGameDev/Anastasis/Crown of Thorns", fileName = "CrownOfThornsAbility")]
    public class CrownOfThornsAbilityConfig : AbilityConfig
    {
        [SerializeField] CrownOfThornsLevel[] _levels;

        public CrownOfThornsLevel[] Levels => _levels;
    }
}
