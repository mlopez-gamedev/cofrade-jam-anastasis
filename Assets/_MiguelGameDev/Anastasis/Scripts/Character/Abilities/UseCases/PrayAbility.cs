namespace MiguelGameDev.Anastasis
{
    public class PrayAbility : Ability
    {
        public PrayAbility(PrayAbilityConfig config) : base(config)
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
