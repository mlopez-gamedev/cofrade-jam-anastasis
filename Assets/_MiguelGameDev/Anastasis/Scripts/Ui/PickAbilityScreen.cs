using Cysharp.Threading.Tasks;
using DG.Tweening;
using I2.Loc;
using MiguelGameDev.Generic.Extensions;
using TMPro;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class PickAbilityScreen : MonoBehaviour
    {
        [SerializeField] AbilityPanel[] _abilityPanels;
        [SerializeField] CanvasGroup _canvasGroup;
        [SerializeField] TMP_Text _actionText;

        [SerializeField] Localize _abilityNameText;
        [SerializeField] Localize _abilityDescriptionText;

        AbilityPanel[] _availableAbilityPanels;
        private int _selectedAbilityPanelIndex;
        private float _nextSelectTime;

        private bool _checkInput;

        private InitGameUseCase _initGameUseCase;

        private void Awake()
        {
            _canvasGroup.gameObject.SetActive(false);
            _actionText.gameObject.SetActive(false);
        }

        public async UniTask<AbilityConfig> PickAbility(CharacterAbilities playerAbilities, AbilityConfig[] abilities)
        {
            _availableAbilityPanels = new AbilityPanel[abilities.Length];

            int i = 0;
            for (; i < abilities.Length; ++i)
            {
                _availableAbilityPanels[i] = _abilityPanels[i];
                _availableAbilityPanels[i].Setup(playerAbilities, abilities[i]);
            }

            for (; i < _abilityPanels.Length; ++i)
            {
                _abilityPanels[i].gameObject.SetActive(false);
            }

            _availableAbilityPanels[0].Select();

            await Show();

            _checkInput = true;
            _actionText.gameObject.SetActive(true);
            while (_checkInput)
            {
                await UniTask.Yield();
            }
            await Hide();
            return _availableAbilityPanels[_selectedAbilityPanelIndex].Ability;
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

        private void Update()
        {
            if (!_checkInput)
            {
                return;
            }

            var input = Input.GetAxis("Horizontal");
            if (input > 0)
            {
                SelectNext();
            }
            else if (input < 0)
            {
                SelectPrevious();
            }
            else if (Input.GetButton("Action"))
            {
                _actionText.gameObject.SetActive(false);
                _checkInput = false;
            }
            else
            {
                _nextSelectTime = 0;
            }
        }

        private void SelectNext()
        {
            if (Time.unscaledTime < _nextSelectTime)
            {
                return;
            }

            var index = _selectedAbilityPanelIndex + 1;
            if (index >= _availableAbilityPanels.Length)
            {
                index = 0;
            }

            Select(index);
            _nextSelectTime = Time.unscaledTime + 0.5f;
        }

        private void SelectPrevious()
        {
            if (Time.unscaledTime < _nextSelectTime)
            {
                return;
            }

            var index = _selectedAbilityPanelIndex - 1;
            if (index < 0)
            {
                index = _availableAbilityPanels.Length - 1;
            }

            Select(index);
            _nextSelectTime = Time.unscaledTime + 0.5f;
        }

        private void Select(int index)
        {
            _availableAbilityPanels[_selectedAbilityPanelIndex].Unselect();
            _selectedAbilityPanelIndex = index;
            _availableAbilityPanels[_selectedAbilityPanelIndex].Select();

            _abilityNameText.SetTerm(
                    _availableAbilityPanels[_selectedAbilityPanelIndex].Ability.TitleTerm);
            _abilityDescriptionText.SetTerm(
                    _availableAbilityPanels[_selectedAbilityPanelIndex].Ability.DescriptionTerm);
        }
    }
}
