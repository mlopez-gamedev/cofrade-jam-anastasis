using Cysharp.Threading.Tasks;
using DG.Tweening;
using MiguelGameDev.Generic.Extensions;
using TMPro;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class TitleScreen : MonoBehaviour
    {
        [SerializeField] CanvasGroup _canvasGroup;
        [SerializeField] TMP_Text _nextText;

        private bool _checkInput;

        private InitGameUseCase _initGameUseCase;

        private void Awake()
        {
            _nextText.gameObject.SetActive(false);
            _canvasGroup.gameObject.SetActive(true);
        }

        public void Setup(InitGameUseCase initGameUseCase)
        {
            _initGameUseCase = initGameUseCase;
        }

        public void Init()
        {
            _nextText.gameObject.SetActive(true);
            _checkInput = true;
        }

        public async UniTask Hide()
        {
            await _canvasGroup.DOFade(0f, 1f).AsAUniTask();
            _canvasGroup.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (!_checkInput)
            {
                return;
            }

            if (Input.GetButtonDown("Action"))
            {
                _checkInput = false;
                _initGameUseCase.InitGame();
            }
        }
    }
}