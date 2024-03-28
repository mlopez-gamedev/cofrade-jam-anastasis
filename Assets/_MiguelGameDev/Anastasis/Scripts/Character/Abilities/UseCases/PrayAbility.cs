namespace MiguelGameDev.Anastasis
{
    public class PrayAbility : Ability
    {
        public override int MaxLevel => throw new System.NotImplementedException();

        public PrayAbility(CharacterAbilities owner, PrayAbilityConfig config) : base(owner, config)
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
