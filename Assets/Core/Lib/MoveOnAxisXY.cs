using Lib;
using UnityEngine;

namespace Core.Lib
{
    public class MoveOnAxisXY : MonoConstruct
    {
        [SerializeField] private Transform _target;
        private Camera _camera;

        private void Awake() => _camera = context.Resolve<Camera>();

        public void FixedUpdate()
        {
            _target.localPosition = Vector3.zero;
            _target.LookAt(_camera.transform);

            var origin = _target.position;
            var direction = _target.forward;

            if (direction.z == 0)
                return;

            float rate = -origin.z / direction.z;
            var intersectionPoint = origin + rate * direction;
            _target.position = intersectionPoint;
        }
    }
}