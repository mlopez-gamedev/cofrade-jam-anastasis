using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class CharacterAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        [SerializeField] private AudioClip[] _hurtClip;
        [SerializeField] private AudioClip[] _dieClip;

        private void Awake()
        {
            _audioSource.loop = false;
            _audioSource.playOnAwake = false;
        }

        public void PlayHurtAudio()
        {
            if (_hurtClip.Length == 0)
            {
                return;
            }
            _audioSource.PlayOneShot(_hurtClip[Random.Range(0, _hurtClip.Length)]);
        }

        public void PlayDieAudio()
        {
            if (_dieClip.Length == 0)
            {
                return;
            }
            _audioSource.PlayOneShot(_dieClip[Random.Range(0, _dieClip.Length)]);
        }
    }
}