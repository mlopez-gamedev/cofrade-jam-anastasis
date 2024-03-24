namespace MiguelGameDev.Anastasis
{
    public class JesusAnimation : CharacterAnimation
    {
        public override void WakeUp()
        {
            base.WakeUp();
            _animator.SetBool("isPraying", false);
        }
    }
}