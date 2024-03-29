using System;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class PlayerGoals
    {
        private EnemyFacade _target;

        public EnemyFacade Target {
            get => _target;
            set {
                if (value == _target)
                {
                    return;
                }

                _target = value;
            }
        }

        public PlayerGoals()
        {
        }
    }
}
