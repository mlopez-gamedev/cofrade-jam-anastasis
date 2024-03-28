using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    [CreateAssetMenu(menuName = "MiguelGameDev/Anastasis/Holy Fire", fileName = "HolyFireAbility")]
    public class HolyFireAbilityConfig : AbilityConfig
    {
        [SerializeField] HolyFireAvatar _holyFireAvatar;
        [SerializeField] HolyFireLevel[] _levels;

        public HolyFireAvatar HolyFireAvatar => _holyFireAvatar;
        public HolyFireLevel[] Levels => _levels;
    }
}
