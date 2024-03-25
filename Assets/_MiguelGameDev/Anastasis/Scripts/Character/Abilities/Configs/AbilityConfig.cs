using UnityEngine;

namespace MiguelGameDev.Anastasis
{

    public abstract class AbilityConfig : ScriptableObject
    {
        [SerializeField] private string _key;
        [SerializeField] private EAbilityType _type;
        [SerializeField, I2.Loc.TermsPopup("Ability.")] private string _titleTerm;
        [SerializeField, I2.Loc.TermsPopup("Ability.")] private string _descriptionTerm;
        [SerializeField] private Sprite _icon;
        [SerializeField] private int _maxLevel = 10;

        public string Key => _key;
        public int Id => _key.GetHashCode();
        public EAbilityType Type => _type;
        public string TitleTerm => _titleTerm;
        public string DescriptionTerm => _descriptionTerm;
        public Sprite Icon => _icon;
        public int MaxLevel => _maxLevel;
    }
}
