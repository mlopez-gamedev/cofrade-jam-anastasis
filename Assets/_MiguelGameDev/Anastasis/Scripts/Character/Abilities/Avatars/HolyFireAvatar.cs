using MEC;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{

    public class HolyFireAvatar : MonoBehaviour
    {
        [SerializeField] float _avatarSizeMultiplier = 0.5f;

        private HolyFireAbility _ability;
        private bool _enable = false;

        public void Setup(HolyFireAbility ability)
        {
            _ability = ability;

            ChangeAvatarSize();

            Timing.CallDelayed(0.5f, Cast, gameObject);
        }

        private void Cast()
        {
            _enable = true;
            Destroy(gameObject, _ability.Duration.Value);
        }

    private void ChangeAvatarSize()
        {
            var avatarSize = _ability.Size.Value * _avatarSizeMultiplier;
            transform.localScale = new Vector3(avatarSize, 1f, avatarSize);
        }

        private void Update()
        {
            transform.rotation = Quaternion.identity;
        }

        private void OnTriggerStay(Collider other)
        {
            if (!_enable)
            {
                return;
            }
            _ability.TryMakeDamage(other);
        }
    }
}
