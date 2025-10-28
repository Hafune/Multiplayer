using Core.Lib.Services;
using Lib;
using Reflex;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core.Services
{
    public class AimService : IInitializableService
    {
        private VisualElement _aim;
        private Camera _camera;

        public void InitializeService(Context context) => _camera = context.Resolve<Camera>();

        public void SetAim(VisualElement aim) => _aim = aim;

        public Vector3 GetAimPosition()
        {
            var ray = _aim.CalculateRay(_camera);
            const float MAX_CAST_DISTANCE = 50;
            if (!Physics.Raycast(ray, out var hit, MAX_CAST_DISTANCE, LayerMask.GetMask("Default")))
                hit.point = ray.origin + ray.direction * MAX_CAST_DISTANCE;

            return hit.point;
        }
    }
}