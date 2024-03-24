using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    [CreateAssetMenu(menuName = "MiguelGameDev/Anastasis/Camera Settings", fileName = "CameraSettings")]
    public class FollowerCameraSettings : ScriptableObject
    {
        [SerializeField] private Vector3 _cameraOffset;
        [SerializeField] private Vector3 _lookAtOffset;
        [SerializeField] private float _smoothSpeed = 1f;
        [SerializeField] private float _smoothAngularSpeed = 4f;
        [SerializeField] private float _targetLookAtMaxDistance = 10f;
        [SerializeField] private float _maxDistance = 20;


        public Vector3 CameraOffset => _cameraOffset;
        public Vector3 LookAtOffset => _lookAtOffset;
        public float SmoothSpeed => _smoothSpeed;
        public float SmoothAngularSpeed => _smoothAngularSpeed;
        public float TargetLookAtMaxDistance => _targetLookAtMaxDistance;
        public float MaxDistance => _maxDistance;
    }
}
