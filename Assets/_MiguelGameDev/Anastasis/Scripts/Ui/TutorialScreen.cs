using Cysharp.Threading.Tasks;
using DG.Tweening;
using MiguelGameDev.Generic.Extensions;
using System;
using TMPro;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class TutorialScreen : MonoBehaviour
    {
        [SerializeField] CanvasGroup _canvasGroup;
        [SerializeField] TMP_Text _nextText;

        private AudioService _audio;
        private bool _checkInput;

        private void Awake()
        {
            _canvasGroup.gameObject.SetActive(false);
            _nextText.gameObject.SetActive(false);
        }

        internal void Setup(AudioService audio)
        {
            _audio = audio;
        }

        public async UniTask ShowTutorial()
        {
            await Show();

            _checkInput = true;
            _nextText.gameObject.SetActive(true);
            while (_checkInput)
            {
                await UniTask.Yield();
            }
            await Hide();
        }

        private async UniTask Show()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.gameObject.SetActive(true);
            await _canvasGroup.DOFade(1f, 0.2f).AsAUniTask();
        }

        private async UniTask Hide()
        {
            await _canvasGroup.DOFade(0f, 0.2f).AsAUniTask();
            _canvasGroup.gameObject.SetActive(false);
        }

        void Update()
        {
            if (!_checkInput)
            {
                return;
            }

            if (Input.GetButtonDown("Action"))
            {
                _checkInput = false;
                _audio.PlayClickSfx();
                _nextText.gameObject.SetActive(false);
            }
        }
    }
}