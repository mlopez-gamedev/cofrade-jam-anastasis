using Sirenix.OdinInspector;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{

    public abstract class AbilityConfig : ScriptableObject
    {
        [SerializeField, BoxGroup("Ability")] private string _key;
        [SerializeField, BoxGroup("Ability")] private EAbilityType _type;
        [SerializeField, BoxGroup("Ability"), I2.Loc.TermsPopup(".Title")] private string _titleTerm;
        [SerializeField, BoxGroup("Ability"), I2.Loc.TermsPopup(".Description")] private string _descriptionTerm;
        [SerializeField, BoxGroup("Ability"), PreviewField(Height = 256)] private Sprite _icon;

        public string Key => _key;
        public int Id => _key.GetHashCode();
        public EAbilityType Type => _type;
        public string TitleTerm => _titleTerm;
        public string DescriptionTerm => _descriptionTerm;
        public Sprite Icon => _icon;
    }
}
