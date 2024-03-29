using System;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class CharacterUi : MonoBehaviour
    {
        [SerializeField] BillboardCanvas _billboard;
        [SerializeField] IntegerProgressBar _healthProgressBar;

        public void Setup(Camera camera, IntegerAttribute maxHealth, IntegerAttribute health)
        {
            _billboard.Setup(camera);
            _healthProgressBar.Bind(maxHealth, health);
        }
    }
}
