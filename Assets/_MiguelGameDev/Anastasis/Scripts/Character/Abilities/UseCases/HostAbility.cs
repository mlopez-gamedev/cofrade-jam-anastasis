namespace MiguelGameDev.Anastasis
{

    public class HostAbility : Ability
    {
        public override int MaxLevel => throw new System.NotImplementedException();

        public HostAbility(CharacterAbilities owner, HostAbilityConfig config) : base(owner, config)
        {

        }

        protected override void ApplyUpgrade()
        {

        }

        public override bool Update()
        {
            return false;
        }
    }
}
