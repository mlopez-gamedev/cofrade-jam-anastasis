using DG.Tweening;
using MiguelGameDev.Generic.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{


    public class CameraPositioner : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _cameraTransform;

        private Vector3 _titlePosition;
        private Vector3 _gamePosition;

        public void Setup(Vector3 titlePosition, Vector3 gamePosition)
        {
            _titlePosition = titlePosition;
            _gamePosition = gamePosition;
        }

        public void SetAsTitlePosition()
        {
            _cameraTransform.position = _titlePosition;
        }

        public Task MoveToTitlePosition(float speed)
        {
            var duration = Vector3.Distance(_cameraTransform.position, _titlePosition) / speed;
            return _cameraTransform.DOMove(_titlePosition, duration).SetEase(Ease.InOutQuad).AsATask();
        }

        public Task MoveToGamePosition(float speed)
        {
            var duration = Vector3.Distance(_cameraTransform.position, _gamePosition) / speed;
            return _cameraTransform.DOMove(_gamePosition, duration).SetEase(Ease.InOutQuad).AsATask();
        }
    }
}