using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class CharacterAttacker : MonoBehaviour
    {
        [SerializeField]


        private int _teamId;
        public int TeamId => _teamId;

        public void Setup(int teamId, EnemyAttackUseCase attackUseCase)
        {

        }
    }
}
