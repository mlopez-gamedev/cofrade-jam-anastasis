using MiguelGameDev.Anastasis;

public readonly struct PlayerAttributes
{
    public FloatAttribute Speed { get; }
    public IntegerAttribute MaxHealth { get; }
    public IntegerAttribute CurrentHealth { get; }
    public FloatAttribute DamageMultiplier { get; }
    public FloatAttribute InvulnerabilityDuration { get; }

    public PlayerAttributes(FloatAttribute speed, IntegerAttribute maxHealth, IntegerAttribute currentHealth,
            FloatAttribute damageMultiplier, FloatAttribute invulnerabilityDuration)
    {
        Speed = speed;
        MaxHealth = maxHealth;
        CurrentHealth = currentHealth;
        DamageMultiplier = damageMultiplier;
        InvulnerabilityDuration = invulnerabilityDuration;
    }
}
