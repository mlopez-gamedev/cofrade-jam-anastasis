using UnityEngine;
using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace MiguelGameDev.Anastasis
{
    public class IntegerProgressBar : MonoBehaviour
    {
        [SerializeField] private RectTransform _downBar;
        [SerializeField] private RectTransform _upBar;
        [SerializeField] private bool _useFrameLines;
        [SerializeField, ShowIf("_useFrameLines")] private RectTransform _frameLineContainer;
        [SerializeField, ShowIf("_useFrameLines")] private RectTransform _frameLinePrefab;
        [SerializeField, SuffixLabel("HP/second")] private float _progressSpeed = 5f;
        [SerializeField] private float _frameSize = 1f;
        [SerializeField] private bool _hideOnEmpty;

        [ShowInInspector, HideInEditorMode, BoxGroup("Game State")] private IntegerAttribute _maxValue;
        [ShowInInspector, HideInEditorMode, BoxGroup("Game State")] private IntegerAttribute _value;

        private List<RectTransform> _createdFrameLines;
        private Tween _tween;

        public void Bind(IntegerAttribute maxValue, IntegerAttribute value)
        {
            _maxValue = maxValue;
            _value = value;

            _maxValue.Subscribe(OnMaxValueChange);
            _value.Subscribe(OnValueChange);

            //InitFrameLanes();
        }

        private void InitFrameLanes()
        {
            if (!_useFrameLines)
            {
                return;
            }
            _createdFrameLines = new List<RectTransform>();
            CreateFrameLines();
        }

        private void CreateFrameLines()
        {
            var positionLimit = 0.95f;
            var gapsAmount = Mathf.Ceil(_maxValue.Value / _frameSize);
            var gapAnchorSize = 1f / gapsAmount;

            var currentPosition = 0f;
            int i;
            for (i = 0; i < gapsAmount; i++)
            {
                currentPosition += gapAnchorSize;
                if (currentPosition > positionLimit)
                {
                    return;
                }

                SetFrameLine(i, currentPosition);
            }

            CleanFrameLine(i);
        }

        private void SetFrameLine(int index, float position)
        {
            RectTransform line;
            if (index < _createdFrameLines.Count)
            {
                line = _createdFrameLines[index];
                line.gameObject.SetActive(true);
                line.SetAnchoredPositionX(position);
                return;
            }

            line = Instantiate(_frameLinePrefab, _frameLineContainer);
            line.SetAnchoredPositionX(position);
        }

        private void CleanFrameLine(int fromIndex)
        {
            for (; fromIndex < _createdFrameLines.Count; ++fromIndex)
            {
                _createdFrameLines[fromIndex].gameObject.SetActive(false);
            }
        }


        private void OnMaxValueChange(int diff)
        {
            if (_tween != null)
            {
                _tween.Kill(true);
            }

            var normalizedValue = Mathf.Clamp01((float)_value.Value / _maxValue.Value);
            _downBar.SetAnchoredPositionX(normalizedValue);
            _upBar.anchorMax = _downBar.anchorMax;

            UpdateFrameLines();
        }

        private void UpdateFrameLines()
        {
            if (!_useFrameLines)
            {
                return;
            }

            if (_createdFrameLines == null)
            {
                InitFrameLanes();
                return;
            }

            CreateFrameLines();
        }

        private void OnValueChange(int diff)
        {
            var normalizedValue = Mathf.Clamp01((float)_value.Value / _maxValue.Value);

            if (diff > 0)
            {
                ProgressUp(normalizedValue, diff);
                return;
            }

            ProgressDown(normalizedValue, -diff);
        }

        private void ProgressUp(float normalizedValue, float distance)
        {
            if (_tween != null)
            {
                _tween.Kill(true);
            }
            if (_hideOnEmpty)
            {
                gameObject.SetActive(true);
            }
            _downBar.SetAnchoredPositionX(normalizedValue);
            
            var duration = distance * _progressSpeed;

            _tween = _upBar.DOAnchorMax(_downBar.anchorMax, duration).SetEase(Ease.InQuad);
        }

        private void ProgressDown(float normalizedValue, float distance)
        {
            if (_tween != null)
            {
                _tween.Kill(true);
            }

            _upBar.SetAnchoredPositionX(normalizedValue);

            var duration = distance / _progressSpeed;

            _tween = _downBar.DOAnchorMax(_upBar.anchorMax, duration).SetEase(Ease.InQuad).OnComplete(CheckHide);
        }

        private void CheckHide()
        {
            if (!_hideOnEmpty)
            {
                return;
            }

            if (_value.Value <= 0)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            _maxValue.Unsubscribe(OnMaxValueChange);
            _value.Unsubscribe(OnValueChange);
        }
    }
}
