using Sirenix.Utilities;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class WorldCenteredMap : Map
    {
        public override void Tick()
        {
            UpdatePlayer();
            UpdateTarget();
        }

        private void UpdatePlayer()
        {
            UpdateMarker(_player, _playerMarker);
        }

        private void UpdateTarget()
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

            UpdateMarker(_playerGoals.Target, _targetMarker);
        }

        private void UpdateMarker(Transform actor, RectTransform marker)
        {
            var actorPosition = actor.position.XZToVector2();
            var actorMarkerPosition = actorPosition * _mapScale;

            var finalTargetMarkerPosition = actorMarkerPosition.Clamp(-_halfMapSize, _halfMapSize);
            if (finalTargetMarkerPosition != actorMarkerPosition)
            {
                _targetMarker.SetScale(0.5f);
            }
            else
            {
                _targetMarker.SetScale(1f);
            }

            marker.anchoredPosition = finalTargetMarkerPosition;
        }
    }
}
