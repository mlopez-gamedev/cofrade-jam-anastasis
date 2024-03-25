namespace MiguelGameDev.Anastasis
{

    public class BloodAbility : Ability
    {
        public BloodAbility(BloodAbilityConfig config) : base(config)
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
