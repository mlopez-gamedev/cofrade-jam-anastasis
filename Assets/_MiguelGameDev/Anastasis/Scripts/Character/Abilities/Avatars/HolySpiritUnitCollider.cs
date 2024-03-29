using UnityEngine;

namespace MiguelGameDev.Anastasis
{

    public class HolySpiritUnitCollider : MonoBehaviour
    {
        private HolySpiritAbility _ability;

        public void Setup(HolySpiritAbility ability)
        {
            _ability = ability;
        }

        private void OnTriggerEnter(Collider other)
        {
            _ability.TryAddTarget(transform, other);
        }

        private void OnTriggerExit(Collider other)
        {
            _ability.TryRemoveTarget(other);
        }
    }
}
