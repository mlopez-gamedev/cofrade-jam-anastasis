using System;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class CharacterUi : MonoBehaviour
    {
        [SerializeField] BillboardCanvas _billboard;
        //[SerializeField] IntegerAttributeText _healthText;
        [SerializeField] IntegerProgressBar _healthProgressBar;

        public void Setup(Camera camera, IntegerAttribute maxHealth, IntegerAttribute health)
        {
            _billboard.Setup(Camera.main);
            //_healthText.Bind(health);
            _healthProgressBar.Bind(maxHealth, health);
        }
    }
}
