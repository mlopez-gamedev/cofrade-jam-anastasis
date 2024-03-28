namespace MiguelGameDev.Anastasis
{

    public class BloodAbility : Ability
    {
        public override int MaxLevel => throw new System.NotImplementedException();

        public BloodAbility(CharacterAbilities owner, BloodAbilityConfig config) : base(owner, config)
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
