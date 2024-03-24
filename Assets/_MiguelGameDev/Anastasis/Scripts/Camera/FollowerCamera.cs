using Sirenix.OdinInspector;
using UnityEngine;
using System;

namespace MiguelGameDev.Anastasis
{

    public class FollowerCamera : MonoBehaviour
    {
        private Transform _transform;

        [ShowInInspector, HideInEditorMode, BoxGroup("Game State")]
        private float _smoothSpeed;

        [ShowInInspector, HideInEditorMode, BoxGroup("Game State")]
        private float _smoothAngularSpeed;

        [ShowInInspector, HideInEditorMode, BoxGroup("Game State")]
        private Vector3 _cameraOffset;

        [ShowInInspector, HideInEditorMode, BoxGroup("Game State")]
        private Vector3 _lookAtOffset;

        [ShowInInspector, HideInEditorMode, BoxGroup("Game State")]
        private float _targetLookAtMaxDistance;

        [ShowInInspector, HideInEditorMode, BoxGroup("Game State")]
        private float _targetLookAtMaxDistanceSqr;

        [ShowInInspector, HideInEditorMode, BoxGroup("Game State")]
        private float _maxDistanceSqr;

        private Transform _target;
        private Vector3 _targetLastPosition;

        private bool _move;
        private bool _rotate;

        private void Awake()
        {
            _transform = transform;
        }

        public void Setup(FollowerCameraSettings config)
        {
            _smoothSpeed = config.SmoothSpeed;
            _smoothAngularSpeed = config.SmoothAngularSpeed;
            _targetLookAtMaxDistance = config.TargetLookAtMaxDistance;
            _targetLookAtMaxDistanceSqr = _targetLookAtMaxDistance * _targetLookAtMaxDistance;
            _cameraOffset = config.CameraOffset;
            _lookAtOffset = config.LookAtOffset;
            _maxDistanceSqr = config.MaxDistance * config.MaxDistance;
        }

        public void SetTarget(Transform target, bool move, bool rotate)
        {
            _target = target;
            _targetLastPosition = _target.position + _cameraOffset;

            _move = move;
            _rotate = rotate;

            //_transform.position = _targetLastPosition;
            _transform.LookAt(_target.position + _lookAtOffset);
        }

        public void SetMove(bool move)
        {
            _move = move;
        }

        public void SetRotate (bool rotate)
        {
            _rotate = rotate;
        }

        private void ReleaseTarget()
        {
            _target = null;
        }

        private void Update()
        {
            if (_target == null)
            {
                return;
            }

            Move();
            Rotate();

            void Move()
            {
                if (!_move)
                {
                    return;
                }

                var followPosition = _target.position + _cameraOffset;
                var direction = followPosition - _transform.position;
                if (direction.sqrMagnitude < _maxDistanceSqr)
                {
                    _transform.position = Vector3.Slerp(transform.position, followPosition, _smoothSpeed * Time.deltaTime);
                }
                else
                {
                    _transform.Translate(followPosition - _targetLastPosition, Space.World); // Match speed with target
                }
                _targetLastPosition = followPosition;
            }

            void Rotate()
            {
                if (!_rotate)
                {
                    return;
                }

                var lookAtPosition = _target.position + _lookAtOffset;

                var lookAtVector = lookAtPosition - _target.position;
                if (lookAtVector.sqrMagnitude > _targetLookAtMaxDistanceSqr)
                {
                    lookAtPosition = _target.position + lookAtVector.normalized * _targetLookAtMaxDistance;
                }

                var rotation = Quaternion.LookRotation(lookAtPosition - _transform.position);

                _transform.rotation = Quaternion.Slerp(_transform.rotation, rotation, _smoothAngularSpeed * Time.deltaTime);
            }
        }

        private void OnDestroy()
        {
            if (_target == null)
            {
                return;
            }

            ReleaseTarget();
        }
    }
}
