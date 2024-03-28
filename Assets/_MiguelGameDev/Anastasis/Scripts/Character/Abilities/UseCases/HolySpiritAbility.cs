namespace MiguelGameDev.Anastasis
{
    public class HolySpiritAbility : Ability
    {
        public override int MaxLevel => throw new System.NotImplementedException();

        public HolySpiritAbility(CharacterAbilities owner, HolySpiritAbilityConfig config) : base(owner, config)
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
