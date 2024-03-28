namespace MiguelGameDev.Anastasis
{
    public readonly struct SacramentShotInfo
    {
        public float Rotation { get; }
        public float Delay { get; }

        public SacramentShotInfo(float rotation, float delay)
        {
            Rotation = rotation;
            Delay = delay;
        }
    }
}
