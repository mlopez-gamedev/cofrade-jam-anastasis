using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    [RequireComponent(typeof(Canvas))]
    public class BillboardCanvas : MonoBehaviour
    {
        private Transform _transform;
        private Transform _cameraTransform;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void Setup(Camera camera)
        {
            _transform = transform;
            _cameraTransform = camera.transform;
            GetComponent<Canvas>().worldCamera = camera;

            gameObject.SetActive(true);
        }

        private void LateUpdate()
        {
            _transform.rotation = _cameraTransform.rotation;
        }
    }
}
