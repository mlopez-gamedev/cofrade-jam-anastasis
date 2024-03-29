using Sirenix.Utilities;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class PlayerCenteredMap : Map
    {
        [SerializeField] protected Vector2 _visibleWorldSize = new Vector2(400f, 300f);

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
            _targetMarker.gameObject.SetActive(true);

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

            _targetMarker.anchoredPosition = targetMarkerPosition;
        }
    }
}
