using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class GameDirector
    {
        private readonly PlayerGoals _playerGoals;

        public GameDirector(PlayerGoals playerGoals)
        {
            _playerGoals = playerGoals;
        }
    }


    public class PlayerGoals
    {
        private Transform _target;

        public Transform Target {
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

        public PlayerGoals(Transform target)
        {
            _target = target;
        }
    }
}
