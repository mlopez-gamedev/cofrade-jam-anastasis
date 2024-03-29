using MiguelGameDev.Anastasis;

public readonly struct CharacterAttributes
{
    public FloatAttribute Speed { get; }
    public IntegerAttribute MaxHealth { get; }
    public IntegerAttribute CurrentHealth { get; }
    public FloatAttribute DamageMultiplier { get; }
    public FloatAttribute InvulnerabilityDuration { get; }

    public CharacterAttributes(FloatAttribute speed, IntegerAttribute maxHealth, IntegerAttribute currentHealth,
            FloatAttribute damageMultiplier, FloatAttribute invulnerabilityDuration)
    {
        Speed = speed;
        MaxHealth = maxHealth;
        CurrentHealth = currentHealth;
        DamageMultiplier = damageMultiplier;
        InvulnerabilityDuration = invulnerabilityDuration;
    }
}
