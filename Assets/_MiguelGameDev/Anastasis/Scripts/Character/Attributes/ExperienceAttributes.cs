using MiguelGameDev.Anastasis;

public readonly struct ExperienceAttributes
{
    public IntegerAttribute CurrentLevel { get; }
    public IntegerAttribute CurrentExperience { get; }
    public IntegerAttribute NextLevelExperience { get; }

    public ExperienceAttributes(IntegerAttribute currentLevel, IntegerAttribute currentExperience, IntegerAttribute nextLevelExperience)
    {
        CurrentLevel = currentLevel;
        CurrentExperience = currentExperience;
        NextLevelExperience = nextLevelExperience;
    }
}
