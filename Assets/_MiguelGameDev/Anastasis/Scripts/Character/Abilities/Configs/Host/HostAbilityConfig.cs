using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    [CreateAssetMenu(menuName = "MiguelGameDev/Anastasis/Host", fileName = "HostAbility")]
    public class HostAbilityConfig : AbilityConfig
    {
        [SerializeField] private ParticleSystem _hostAvatar;
        [SerializeField] private HostLevel[] _levels;

        public ParticleSystem HostAvatar => _hostAvatar;
        public HostLevel[] Levels => _levels;
    }
}
