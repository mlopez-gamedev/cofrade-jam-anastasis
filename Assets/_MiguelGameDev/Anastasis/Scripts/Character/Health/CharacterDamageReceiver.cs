using Sirenix.OdinInspector;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class CharacterDamageReceiver : MonoBehaviour
    {
        [ShowInInspector, HideInEditorMode] private FloatAttribute _invulnerabilityDuration;
        private TakeDamageUseCase _takeDamageUseCase;

        private float _invulnerabilityEndTime;

        public void Setup(FloatAttribute invulnerabilityDuration, TakeDamageUseCase takeDamageUseCase)
        {
            _invulnerabilityDuration = invulnerabilityDuration;
            _takeDamageUseCase = takeDamageUseCase;
        }

        public void TakeDamage(DamageInfo damageInfo)
        {
            if (Time.time < _invulnerabilityEndTime) 
            {
                return;
            }

            if (!_takeDamageUseCase.TakeDamage(damageInfo))
            {
                return;
            }

            _invulnerabilityEndTime = Time.time + _invulnerabilityDuration.Value;
        }
    }
}