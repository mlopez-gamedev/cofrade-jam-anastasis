using Cysharp.Threading.Tasks;
using DG.Tweening;
using MiguelGameDev.Generic.Extensions;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class AudioService : MonoBehaviour
    {
        [SerializeField, BoxGroup("Audio")] AudioSource _uiAudioSource;
        [SerializeField, BoxGroup("Audio")] AudioSource _ambientAudioSource;
        [SerializeField, BoxGroup("Audio")] AudioSource _musicAudioSource;

        [SerializeField, BoxGroup("Audio/Music")] AudioClip _gameMusicClip;
        [SerializeField, BoxGroup("Audio/Music")] AudioClip _winMusicClip;
        [SerializeField, BoxGroup("Audio/Music")] AudioClip _loseMusicClip;

        [SerializeField, BoxGroup("Audio/Ui")] AudioClip _clickSfxClip;
        [SerializeField, BoxGroup("Audio/Ui")] AudioClip _selectSfxClip;

        private Tween _fadeMusicTween;
        private float _defaultMusicVolume;

        public AudioClip GameMusicClip => _gameMusicClip;
        public AudioClip WinMusicClip => _winMusicClip;
        public AudioClip LoseMusicClip => _loseMusicClip;

        private void Awake()
        {
            _defaultMusicVolume = _musicAudioSource.volume;
        }

        public void PlayClickSfx()
        {
            _uiAudioSource.pitch = 1f;
            _uiAudioSource.PlayOneShot(_clickSfxClip);
        }

        internal void PlaySelectSfx()
        {
            _uiAudioSource.pitch = 1f;
            _uiAudioSource.PlayOneShot(_selectSfxClip);
        }

        internal void PlayAmbient()
        {
            _ambientAudioSource.Play();
        }

        public void PlayMusic(AudioClip musicClip)
        {
            if (!_musicAudioSource.isPlaying)
            {
                StartPlayMusic(musicClip);
                return;
            }

            _musicAudioSource.DOFade(0, 0.2f).OnComplete(() =>
            {
                StartPlayMusic(musicClip);
            });
        }

        public UniTask PlayMusic(AudioClip musicClip, float fadeOutDuration)
        {
            if (!_musicAudioSource.isPlaying)
            {
                return StartPlayMusic(musicClip, fadeOutDuration);
            }

            return StartPlayMusic(musicClip, fadeOutDuration);
        }

        private void StartPlayMusic(AudioClip musicClip)
        {
            _fadeMusicTween?.Kill();
            _fadeMusicTween = null;

            _ambientAudioSource.volume = 0.5f;
            _musicAudioSource.volume = _defaultMusicVolume;
            _musicAudioSource.clip = musicClip;
            _musicAudioSource.Play();
        }

        private UniTask StartPlayMusic(AudioClip musicClip, float fadeOutDuration)
        {
            _fadeMusicTween?.Kill();

            _musicAudioSource.volume = 0;
            _musicAudioSource.clip = musicClip;
            _musicAudioSource.Play();

            _fadeMusicTween = DOTween.Sequence()
                .Append(_ambientAudioSource.DOFade(0.5f, fadeOutDuration))
                .Join(_musicAudioSource.DOFade(_defaultMusicVolume, fadeOutDuration))
                .AppendCallback(() =>
                {
                    _fadeMusicTween = null;
                });

            return _fadeMusicTween.AsAUniTask();
        }

        public void StopMusic()
        {
            _fadeMusicTween?.Kill();
            _musicAudioSource.Stop();
        }

        public UniTask StopMusic(float fadeInDuration)
        {
            _fadeMusicTween?.Kill();
            _fadeMusicTween = DOTween.Sequence()
                .Append(_ambientAudioSource.DOFade(1f, fadeInDuration))
                .Join(_musicAudioSource.DOFade(0, fadeInDuration))
                .OnComplete(() =>
                {
                    _musicAudioSource.Stop();
                    _fadeMusicTween = null;
                });
            return _fadeMusicTween.AsAUniTask();
        }
    }
}
