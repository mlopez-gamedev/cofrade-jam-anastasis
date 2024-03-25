namespace MiguelGameDev.Anastasis
{
    public class BlessingAbility : Ability
    {
        public BlessingAbility(BlessingAbilityConfig config) : base(config)
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
