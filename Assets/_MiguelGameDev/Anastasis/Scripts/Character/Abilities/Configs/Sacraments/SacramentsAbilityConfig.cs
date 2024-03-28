using System;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{

    [CreateAssetMenu(menuName = "MiguelGameDev/Anastasis/Sacraments", fileName = "SacramentsAbility")]
    public class SacramentsAbilityConfig : AbilityConfig
    {
        [SerializeField] SacramentAvatar _sacramentAvatar;
        [SerializeField] SacramentsLevel[] _levels;

        public SacramentAvatar SacramentAvatar => _sacramentAvatar;
        public SacramentsLevel[] Levels => _levels;
    }
}
