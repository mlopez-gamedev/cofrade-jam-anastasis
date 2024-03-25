namespace MiguelGameDev.Anastasis
{
    public class AbilityFactory
    {
        public Ability CreateAbility(AbilityConfig abilityConfig)
        {
            if (abilityConfig is BlessingAbilityConfig)
            {
                return new BlessingAbility(abilityConfig as BlessingAbilityConfig);
            }
            else if (abilityConfig is BloodAbilityConfig)
            {
                return new BloodAbility(abilityConfig as BloodAbilityConfig);
            }
            else if (abilityConfig is CrownOfThornsAbilityConfig)
            {
                return new CrownOfThornsAbility(abilityConfig as CrownOfThornsAbilityConfig);
            }
            else if (abilityConfig is HolyFireAbilityConfig)
            {
                return new HolyFireAbility(abilityConfig as HolyFireAbilityConfig);
            }
            else if (abilityConfig is HolySpiritAbilityConfig)
            {
                return new HolySpiritAbility(abilityConfig as HolySpiritAbilityConfig);
            }
            else if (abilityConfig is HostAbilityConfig)
            {
                return new HostAbility(abilityConfig as HostAbilityConfig);
            }
            else if (abilityConfig is SacramentsAbilityConfig)
            {
                return new SacramentsAbility(abilityConfig as SacramentsAbilityConfig);
            }

            return null;
        }
    }
}
