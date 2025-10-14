using UnityEngine;
using UnityEngine.Assertions;

namespace Core.ExternalEntityLogics
{
    public class SetPositionByPointLogic : AbstractEntityLogic
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Transform _point;
        
#if UNITY_EDITOR
        private void Awake()
        {
            Assert.IsNotNull(_target);
            Assert.IsNotNull(_point);
        }
#endif

        public override void Run(int entity) => _target.position = (Vector2)_point.position;
    }
}