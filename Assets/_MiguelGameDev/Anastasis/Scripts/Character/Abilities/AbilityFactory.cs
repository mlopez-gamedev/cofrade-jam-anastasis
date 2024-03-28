namespace MiguelGameDev.Anastasis
{
    public class AbilityFactory
    {
        public Ability CreateAbility(CharacterAbilities characterAbilities, AbilityConfig abilityConfig)
        {
            if (abilityConfig is BlessingAbilityConfig)
            {
                return new BlessingAbility(characterAbilities, abilityConfig as BlessingAbilityConfig);
            }
            else if (abilityConfig is BloodAbilityConfig)
            {
                return new BloodAbility(characterAbilities, abilityConfig as BloodAbilityConfig);
            }
            else if (abilityConfig is CrownOfThornsAbilityConfig)
            {
                return new CrownOfThornsAbility(characterAbilities, abilityConfig as CrownOfThornsAbilityConfig);
            }
            else if (abilityConfig is HolyFireAbilityConfig)
            {
                return new HolyFireAbility(characterAbilities, abilityConfig as HolyFireAbilityConfig);
            }
            else if (abilityConfig is HolySpiritAbilityConfig)
            {
                return new HolySpiritAbility(characterAbilities, abilityConfig as HolySpiritAbilityConfig);
            }
            else if (abilityConfig is HostAbilityConfig)
            {
                return new HostAbility(characterAbilities, abilityConfig as HostAbilityConfig);
            }
            else if (abilityConfig is SacramentsAbilityConfig)
            {
                return new SacramentsAbility(characterAbilities, abilityConfig as SacramentsAbilityConfig);
            }

            return null;
        }
    }
}
