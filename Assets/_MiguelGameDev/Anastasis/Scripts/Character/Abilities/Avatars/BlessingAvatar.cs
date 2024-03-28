using System.Drawing;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{

    public class BlessingAvatar : MonoBehaviour
    {
        [SerializeField] float _avatarSizeMultiplier = 0.5f;

        private BlessingAbility _ability;

        public void Setup(BlessingAbility ability)
        {
            _ability = ability;

            ChangeAvatarSize();
            _ability.Size.Subscribe(OnChangeSize);
        }


        private void OnChangeSize(float _)
        {
            ChangeAvatarSize();
        }

        private void ChangeAvatarSize()
        {
            var avatarSize = _ability.Size.Value * _avatarSizeMultiplier;
            transform.localScale = new Vector3(avatarSize, avatarSize, avatarSize);
        }

        private void Update()
        {
            transform.rotation = Quaternion.identity;
        }

        private void OnTriggerStay(Collider other)
        {
            _ability.TryMakeDamage(other);
        }

        private void OnDestroy()
        {
            _ability?.Size.Unsubscribe(OnChangeSize);
        }
    }
}
