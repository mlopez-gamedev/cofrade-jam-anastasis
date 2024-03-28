using DG.Tweening;
using UnityEditor.Playables;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class HolySpiritUnit : MonoBehaviour
    {
        [SerializeField] HolySpiritUnitCollider _collider;

        private FloatAttribute _cycleDuration;
        private FloatAttribute _minDistance;
        private FloatAttribute _maxDistance;
        private FloatAttribute _angularSpeed;

        private bool _xReverse;
        private bool _yReverse;

        private Tween _xTween;
        private Tween _yTween;

        public void Setup(HolySpiritAbility ability, HolySpiritAbility.HolySpiritUnitData unitData, float rotation)
        {
            _collider.Setup(ability);

            _cycleDuration = unitData.CycleDuration;
            _minDistance = unitData.MinDistance;
            _maxDistance = unitData.MaxDistance;
            _angularSpeed = unitData.AngularSpeed;

            gameObject.SetActive(true);

            ResetPositions(rotation);
        }

        private void NextHorizontal()
        {
            _xReverse = ! _xReverse;
            if (_xReverse)
            {
                NextHorizontalReverse();
                return;
            }

            _xTween = _collider.transform.DOLocalMoveX(_maxDistance.Value, _cycleDuration.Value).SetEase(Ease.InOutSine).OnComplete(NextHorizontal);
        }

        private void NextHorizontalReverse()
        {
            _xTween = _collider.transform.DOLocalMoveX(-_maxDistance.Value, _cycleDuration.Value).SetEase(Ease.InOutSine).OnComplete(NextHorizontal);
        }

        private void NextVertical()
        {
            _yReverse = !_yReverse;
            if (_yReverse)
            {
                NextVerticalReverse();
                return;
            }

            _yTween = _collider.transform.DOLocalMoveZ(_minDistance.Value, _cycleDuration.Value).SetEase(Ease.InOutSine).OnComplete(NextVertical);
        }

        private void NextVerticalReverse()
        {
            _yTween = _collider.transform.DOLocalMoveZ(-_minDistance.Value, _cycleDuration.Value).SetEase(Ease.InOutSine).OnComplete(NextVertical);
        }

        public void Tick()
        {
            transform.Rotate(new Vector3(0, _angularSpeed.Value * Time.deltaTime, 0), Space.Self);
        }

        public void ResetPositions(float rotation)
        {
            transform.localRotation = Quaternion.Euler(0, rotation, 0);

            _xTween?.Kill();
            _yTween?.Kill();
            _collider.transform.localPosition = new Vector3(-_maxDistance.Value, 0, -_minDistance.Value);

            _xReverse = false;
            _yReverse = false;

            _xTween = _collider.transform.DOLocalMoveX(_maxDistance.Value, _cycleDuration.Value).SetEase(Ease.InOutSine).OnComplete(NextHorizontal);
            _yTween = _collider.transform.DOLocalMoveZ(_minDistance.Value, _cycleDuration.Value).SetEase(Ease.InOutSine).OnComplete(NextVertical);

            _xTween.fullPosition = _cycleDuration.Value / 2f;
        }
    }
}
