namespace MiguelGameDev.Anastasis
{

    public class HostAbility : Ability
    {
        public HostAbility(HostAbilityConfig config) : base(config)
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
