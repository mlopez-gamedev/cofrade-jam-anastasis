namespace MiguelGameDev.Anastasis
{
    public abstract class CharacterInput
    {
        protected bool _enable;
        public bool Enable => _enable;

        public abstract void Init();
        public abstract void Update();
        public void SetEnable(bool enable)
        {
            _enable = enable;
        }
    }
}