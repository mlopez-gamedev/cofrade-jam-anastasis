using DG.Tweening;
using I2.Loc;
using MiguelGameDev.Generic.Extensions;
using System.Globalization;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class StoryScreen : MonoBehaviour
    {
        [SerializeField] CanvasGroup _canvasGroup;
        [SerializeField] Localize _storyTextLocalize;
        [SerializeField] TextVisibilityAnimation _storyVisibilityAnimation;
        [SerializeField] TMP_Text _nextText;

        private bool _checkInput;

        private void Awake()
        {
            _canvasGroup.gameObject.SetActive(false);
            _nextText.gameObject.SetActive(false);
            _storyTextLocalize.SetTerm(null);
        }

        public async Task ShowStory(string storyKey)
        {
            await Show();
            await PlayStory(storyKey);

            while (_checkInput)
            {
                await Task.Yield();
            }
            await Hide();
        }

        private Task PlayStory(string storyKey)
        {
            TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();

            _storyTextLocalize.SetTerm(storyKey);
            _storyTextLocalize.gameObject.SetActive(true);
            _storyVisibilityAnimation.OnComplete += OnStoryEnds;
            _storyVisibilityAnimation.Play();
            _checkInput = true;

            return taskCompletionSource.Task;


            void OnStoryEnds()
            {
                _storyVisibilityAnimation.OnComplete -= OnStoryEnds;

                _nextText.gameObject.SetActive(true);

                taskCompletionSource.SetResult(true);
            }
        }

        private async Task Show()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.gameObject.SetActive(true);
            await _canvasGroup.DOFade(1f, 1f).AsATask();
        }

        private async Task Hide()
        {
            _storyTextLocalize.gameObject.SetActive(false);
            _storyTextLocalize.SetTerm(null);
            await _canvasGroup.DOFade(0f, 1f).AsATask();
            _canvasGroup.gameObject.SetActive(false);
        }

        void Update()
        {
            if (!_checkInput)
            {
                return;
            }

            if (Input.anyKeyDown)
            {
                if (!_storyVisibilityAnimation.IsComplete)
                {
                    _storyVisibilityAnimation.Complete();
                    return;
                }

                _checkInput = false;
                _nextText.gameObject.SetActive(false);
            }
        }
    }
}