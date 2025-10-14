using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class RestoreRotationLogic : AbstractEntityLogic
    {
        [SerializeField] private Transform _target;
        private Quaternion _rotation;

        private void Awake() => _rotation = _target.localRotation;

        public override void Run(int entity) => _target.localRotation = _rotation;
    }
}