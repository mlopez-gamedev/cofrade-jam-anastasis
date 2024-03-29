using Sirenix.Utilities;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class PlayerCenteredMap : Map
    {
        private Vector2 _backgroundOffset;

        public override void Tick()
        {
            UpdateBackgroundOffset();
            UpdateTargets();
        }

        private void UpdateBackgroundOffset()
        {
            var playerPosition = _player.position.XZToVector2();
            _backgroundOffset = -playerPosition * _mapScale;
        }

        private void UpdateTargets()
        {
            if (_playerGoals.Target == null)
            {
                _targetMarker.gameObject.SetActive(false);
                return;
            }
            if (!_targetMarker.gameObject.activeSelf)
            {
                _targetMarker.gameObject.SetActive(true);
            }

            var targetPosition = _playerGoals.Target.position.XZToVector2();
            var targetMarkerPosition = targetPosition * _mapScale + _backgroundOffset;
            
            var finalTargetMarkerPosition = targetMarkerPosition.Clamp(-_halfMapSize, _halfMapSize);
            if (finalTargetMarkerPosition != targetMarkerPosition)
            {
                _targetMarker.SetScale(0.5f);
            }
            else
            {
                _targetMarker.SetScale(1f);
            }

            _targetMarker.anchoredPosition = finalTargetMarkerPosition;
        }
    }
}
