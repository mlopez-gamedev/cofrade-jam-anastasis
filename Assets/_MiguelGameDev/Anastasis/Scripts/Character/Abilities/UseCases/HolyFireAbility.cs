namespace MiguelGameDev.Anastasis
{
    public class HolyFireAbility : Ability
    {
        public HolyFireAbility(HolyFireAbilityConfig config) : base(config)
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
