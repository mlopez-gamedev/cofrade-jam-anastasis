using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class CharacterAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        [SerializeField] private AudioClip[] _hurtClip;
        [SerializeField] private AudioClip _dieClip;

        private void Awake()
        {
            _audioSource.loop = false;
            _audioSource.playOnAwake = false;
        }

        public void PlayHurtAudio()
        {
            _audioSource.PlayOneShot(_hurtClip[Random.Range(0, _hurtClip.Length)]);
        }

        public void PlayDieAudio()
        {
            _audioSource.PlayOneShot(_dieClip);
        }
    }
}