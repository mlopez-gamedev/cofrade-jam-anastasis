namespace MiguelGameDev.Anastasis
{
    public class SacramentsAbility : Ability
    {
        public override int MaxLevel => throw new System.NotImplementedException();

        public SacramentsAbility(CharacterAbilities owner, SacramentsAbilityConfig config) : base(owner, config)
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
