namespace MiguelGameDev.Anastasis
{
    public class HolySpiritAbility : Ability
    {
        public HolySpiritAbility(HolySpiritAbilityConfig config) : base(config)
        {

        }

        protected override void ApplyUpgrade()
        {

        }

        public override bool TryExecute()
        {
            return false;
        }
    }
}
