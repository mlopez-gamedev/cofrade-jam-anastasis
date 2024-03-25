using UnityEngine;

namespace MiguelGameDev.Anastasis
{

    public class AbilityPanel : MonoBehaviour
    {
        [SerializeField] private AbilityPanelInfo _selectedInfo;
        [SerializeField] private AbilityPanelInfo _unselectedInfo;

        private AbilityConfig _ability;

        public AbilityConfig Ability => _ability;

        public void Setup(CharacterAbilities playerAbilities, AbilityConfig ability)
        {
            _ability = ability;
            _selectedInfo.Setup(playerAbilities, ability);
            _unselectedInfo.Setup(playerAbilities,ability);
            gameObject.SetActive(true);
        }

        public void Select()
        {
            _unselectedInfo.gameObject.SetActive(false);
            _selectedInfo.gameObject.SetActive(true);
        }

        public void Unselect()
        {
            _unselectedInfo.gameObject.SetActive(true);
            _selectedInfo.gameObject.SetActive(false);
        }
    }
}
