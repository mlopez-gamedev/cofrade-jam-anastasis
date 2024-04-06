using UnityEngine;

namespace MiguelGameDev.Anastasis
{

    [CreateAssetMenu(menuName = "MiguelGameDev/Anastasis/Player Settings", fileName = "PlayerSettings")]
    public class PlayerSettings : CharacterSettings
    {
        [SerializeField] private EquationGrade[] _experienceEquationGrades;

        public EquationGrade[] ExperienceEquationGrades => _experienceEquationGrades;
    }
}