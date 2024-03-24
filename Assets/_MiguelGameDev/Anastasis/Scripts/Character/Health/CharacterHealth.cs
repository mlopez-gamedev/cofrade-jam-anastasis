using Sirenix.OdinInspector;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class CharacterHealth
    {
        [ShowInInspector] private IntegerAttribute _maxHealth;
        [ShowInInspector] private IntegerAttribute _currentHealth;

        public float MaxHealth => _maxHealth.Value;
        public float CurrentHealth => _currentHealth.Value;

        public bool IsDead => _currentHealth.Value <= 0;

        public CharacterHealth(IntegerAttribute maxHealth, IntegerAttribute currentHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = currentHealth;
        }

        public void Resurrect(int health)
        {
            _currentHealth.Value = health;
        }

        public void Heal(int heal)
        {
            _currentHealth.Value = Mathf.Min(_maxHealth.Value, _currentHealth.Value + heal);
        }

        public void TakeDamage(int damage)
        {
            _currentHealth.Value = Mathf.Max(0, _currentHealth.Value - damage);
        }
    }
}