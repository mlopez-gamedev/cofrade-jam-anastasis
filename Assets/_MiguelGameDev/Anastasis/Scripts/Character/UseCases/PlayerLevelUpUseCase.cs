namespace MiguelGameDev.Anastasis
{
    public class PlayerLevelUpUseCase
    {
        private readonly PlayerPickAbilityUseCase _playerPickAbilityUseCase;

        public PlayerLevelUpUseCase(PlayerPickAbilityUseCase playerPickAbilityUseCase)
        {
            _playerPickAbilityUseCase = playerPickAbilityUseCase;
        }

        public void PlayerLevelUp()
        {
            // stop
            // check if ability available
            //    pick and apply ability
            // continue
        }
    }
}