using MEC;
using UnityEngine;
using UnityEngine.Pool;

namespace MiguelGameDev.Anastasis
{
    public class SacramentAvatar : MonoBehaviour
    {
        [SerializeField] Transform _projectile;
        [SerializeField] AudioSource _audioSource;
        [SerializeField] SacramentCollider _collider;

        private SacramentsAbility _ability;
        private ObjectPool<SacramentAvatar> _objectPool;

        private bool _enable;
        private float _speed;
        private float _duration;

        private CoroutineHandle _killCoroutine;

        internal void Setup(SacramentsAbility ability, ObjectPool<SacramentAvatar> objectPool)
        {
            _ability = ability;
            _objectPool = objectPool;
            _collider.Setup(this);
        }

        internal void Init(Vector3 position, Quaternion rotation, float forward, float delay)
        {
            position.y = transform.position.y;

            transform.position = position;
            transform.rotation = rotation;

            transform.Rotate(0, forward, 0, Space.Self);
            _projectile.localPosition = new Vector3(0, 0, 1f);

            _duration = _ability.Duration.Value;
            _speed = _ability.Range.Value / _duration;

            if (delay == 0)
            {
                Cast();
                return;
            }

            Timing.CallDelayed(delay, Cast, gameObject);
        }

        private void Cast()
        {
            gameObject.SetActive(true);

            _audioSource.pitch = Random.Range(0.85f, 1.1f);
            _audioSource.Play();

            _enable = true;

            _killCoroutine = Timing.CallDelayed(_duration, Kill, gameObject);
        }

        private void Update()
        {
            if (!_enable)
            {
                return;
            }

            _projectile.position = _projectile.position + _projectile.forward * _speed * Time.deltaTime;
        }

        public void TryMakeDamage(Transform causer, Collider other)
        {
            if (_ability.TryMakeDamage(causer, other))
            {
                Timing.KillCoroutines(_killCoroutine);
                Kill();
            }
        }

        private void Kill()
        {
            _objectPool.Release(this);
        }

        internal void Finish()
        {
            _enable = false;
            gameObject.SetActive(false);
        }
    }
}
