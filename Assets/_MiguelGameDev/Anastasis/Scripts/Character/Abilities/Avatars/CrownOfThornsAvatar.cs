using System;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class CrownOfThornsAvatar : MonoBehaviour
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

        internal void TryReturnDamage(DamageInfo damageInfo)
        {
            if (!gameObject.activeSelf)
            {
                return;
            }

            _ability.ReturnDamage(damageInfo);
        }
    }
}
