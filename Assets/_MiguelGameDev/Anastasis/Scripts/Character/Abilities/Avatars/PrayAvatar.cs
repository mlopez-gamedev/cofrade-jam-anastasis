using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class PrayAvatar : MonoBehaviour
    {
        [SerializeField] ParticleSystem _prayEffect;
        public void Play()
        {
            _prayEffect.Play();
        }
    }
}
