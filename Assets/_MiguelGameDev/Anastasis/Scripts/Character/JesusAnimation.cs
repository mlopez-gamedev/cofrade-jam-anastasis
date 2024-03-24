namespace MiguelGameDev.Anastasis
{
    public class JesusAnimation : CharacterAnimation
    {
        public override void Init()
        {
            base.Init();
            PlayIdle();
        }

        public void PlayIdle()
        {
            _animator.SetBool("isPraying", false);
        }
    }
}