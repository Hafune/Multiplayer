using UnityEngine;

namespace Core.Lib
{
    public class CameraRectUpdater : MonoBehaviour
    {
        [SerializeField] private Vector2 _baseAspectRatio = new Vector2(9, 16);

        private Camera _camera;
        private float _targetAspect;
        private float _windowAspect;
        private float _scaleHeight;
        private float _scaleWidth;

        private void Start()
        {
            _camera = GetComponent<Camera>();
            _targetAspect = _baseAspectRatio.x / _baseAspectRatio.y;
        }

        private void LateUpdate()
        {
            CorrectCameraViewport();
        }

        private void CorrectCameraViewport()
        {
            _windowAspect = (float)Screen.width / (float)Screen.height;
            _scaleHeight = _windowAspect / _targetAspect;

            if (_scaleHeight < 1.0f)
            {
                Rect rect = _camera.rect;
                rect.width = 1.0f;
                rect.height = _scaleHeight;
                rect.x = 0;
                rect.y = (1.0f - _scaleHeight) / 2.0f;
                _camera.rect = rect;
            }
            else
            {
                _scaleWidth = 1.0f / _scaleHeight;
                Rect rect = _camera.rect;
                rect.width = _scaleWidth;
                rect.height = 1.0f;
                rect.x = (1.0f - _scaleWidth) / 2.0f;
                rect.y = 0;
                _camera.rect = rect;
            }
        }
    }
}