using System;
using UnityEngine;
using static UnityEngine.Screen;

namespace Core.Lib.Utils
{
    public class PolygonCollider2dToCameraCorners : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private PolygonCollider2D _collider;
        private int _lastWidth;
        private int _lastHeight;

        private void Start()
        {
            _lastWidth = width;
            _lastHeight = height;
            
            // Преобразуем координаты углов видимой области камеры в локальные координаты уровня
            Span<Vector2> cameraCorners = stackalloc Vector2[]
            {
                GetPositionByViewport(new Vector3(0, 1, 0)),
                GetPositionByViewport(new Vector3(1, 1, 0)),
                GetPositionByViewport(new Vector3(1, 0, 0)),
                GetPositionByViewport(new Vector3(0, 0, 0)),
            };

            _collider.points = cameraCorners.ToArray();
        }
        
        private Vector3 GetPositionByViewport(Vector3 pos)
        {
            var ray = _camera.ViewportPointToRay(pos);
            var t = -ray.origin.z / ray.direction.z;
            var point = ray.origin + t * ray.direction;
            return _collider.transform.InverseTransformPoint(point);
        }
        
        private void FixedUpdate()
        {
            if (_lastWidth == width && _lastHeight == height)
                return;
            
            Start();
        }
    }
}