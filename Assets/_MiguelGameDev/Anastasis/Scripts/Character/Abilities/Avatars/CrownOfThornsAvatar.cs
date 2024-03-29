using System;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class CrownOfThornsAvatar : MonoBehaviour, IDamageEffector
    {
        private CrownOfThornsAbility _ability;

        public void Setup(CrownOfThornsAbility ability)
        {
            _ability = ability;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void DoDamageEffector(DamageInfo damageInfo)
        {
            if (!gameObject.activeSelf)
            {
                return;
            }

            _ability.ReturnDamage(damageInfo);
        }
    }
}
