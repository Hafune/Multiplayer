using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class RestorePositionLogic : AbstractEntityLogic
    {
        [SerializeField] private Transform _target;
        private Vector3 _localPosition;

        private void Awake() => _localPosition = _target.localPosition;

        public override void Run(int entity) => _target.localPosition = _localPosition;
    }
}