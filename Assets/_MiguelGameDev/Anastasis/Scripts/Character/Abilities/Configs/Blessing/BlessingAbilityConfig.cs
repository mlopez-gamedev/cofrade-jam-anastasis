using Sirenix.OdinInspector;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{

    [CreateAssetMenu(menuName = "MiguelGameDev/Anastasis/Blessing", fileName = "BlessingAbility")]
    public class BlessingAbilityConfig : AbilityConfig
    {
        [SerializeField, BoxGroup("Blessing")] BlessingAvatar _avatar;
        [SerializeField, BoxGroup("Blessing")] BlessingLevel[] _levels;

        public BlessingAvatar Avatar  => _avatar;
        public BlessingLevel[] Levels => _levels;
    }
}
