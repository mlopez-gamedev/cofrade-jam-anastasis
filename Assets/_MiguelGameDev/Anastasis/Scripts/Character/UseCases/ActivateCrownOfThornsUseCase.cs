namespace MiguelGameDev.Anastasis
{
    public class ActivateCrownOfThornsUseCase
    {
        private readonly CrownOfThornsAvatar _crownOfThrons;

        public ActivateCrownOfThornsUseCase(CrownOfThornsAvatar crownOfThrons)
        {
            _crownOfThrons = crownOfThrons;
        }

        public CrownOfThornsAvatar ActivateCrownOfThorns(CrownOfThornsAbility ability)
        {
            _crownOfThrons.Setup(ability);
            _crownOfThrons.Show();
            return _crownOfThrons;
        }
    }
}