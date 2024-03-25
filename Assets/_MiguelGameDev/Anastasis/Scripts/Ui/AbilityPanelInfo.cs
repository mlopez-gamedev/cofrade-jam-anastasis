using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MiguelGameDev.Anastasis
{
    public class AbilityPanelInfo : MonoBehaviour
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _levelText;

        public void Setup(CharacterAbilities playerAbilities, AbilityConfig abilityConfig)
        {
            _iconImage.sprite = abilityConfig.Icon;
            _iconImage.gameObject.SetActive(true);

            if (!playerAbilities.HasAbility(abilityConfig.Id))
            {
                _levelText.text = I2.Loc.LocalizationManager.GetTermTranslation("New");
                return;
            }

            var ability = playerAbilities.GetAbility(abilityConfig.Id);
            _levelText.text = string.Format(I2.Loc.LocalizationManager.GetTermTranslation("Level"), 
                    ability.CurrentLevel);
        }
    }
}
