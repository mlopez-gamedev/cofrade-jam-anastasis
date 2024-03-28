using MEC;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class HolyFireAvatar : MonoBehaviour
    {
        [SerializeField] AudioSource _audioSource;
        [SerializeField] float _avatarSizeMultiplier = 0.5f;

        private HolyFireAbility _ability;
        private bool _enable = false;

        public void Setup(HolyFireAbility ability)
        {
            _ability = ability;
        }

        internal void Init(Vector3 position)
        {
            transform.position = position;
            ChangeAvatarSize();
            Timing.CallDelayed(0.5f, Cast, gameObject);
            gameObject.SetActive(true);
        }

        private void Cast()
        {
            _enable = true;
            _audioSource.pitch = Random.Range(0.85f, 1.1f);
            _audioSource.Play();
            Destroy(gameObject, _ability.Duration.Value);
        }

        private void ChangeAvatarSize()
        {
            var avatarSize = _ability.Size.Value * _avatarSizeMultiplier;
            transform.localScale = new Vector3(avatarSize, 1f, avatarSize);
        }

        private void OnTriggerStay(Collider other)
        {
            if (!_enable)
            {
                return;
            }
            _ability.TryMakeDamage(other);
        }

        internal void Finish()
        {
            _enable = false;
            gameObject.SetActive(true);
        }
    }
}
