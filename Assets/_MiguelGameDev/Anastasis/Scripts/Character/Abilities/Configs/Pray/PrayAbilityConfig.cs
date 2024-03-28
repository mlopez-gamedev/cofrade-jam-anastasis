using Unity.VisualScripting;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{


    [CreateAssetMenu(menuName = "MiguelGameDev/Anastasis/Pray", fileName = "PrayAbility")]
    public class PrayAbilityConfig : AbilityConfig
    {
        [SerializeField] PrayAvatar _prayAvatar;
        [SerializeField] PrayLevel[] _levels;

        public PrayAvatar PrayAvatar => _prayAvatar;
        public PrayLevel[] Levels => _levels;
    }
}
