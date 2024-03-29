using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class SacramentCollider : MonoBehaviour
    {
        private SacramentAvatar _avatar;

        public void Setup(SacramentAvatar avatar)
        {
            _avatar = avatar;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Sacrament trying make dmage to " + other);
            _avatar.TryMakeDamage(transform, other);
        }
    }
}
