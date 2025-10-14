using UnityEngine;
using UnityEngine.Assertions;

namespace Core.ExternalEntityLogics
{
    public class SetEnableLogic : AbstractEntityLogic
    {
        [SerializeField] private MonoBehaviour _target;
        [SerializeField] private bool _enabled;
        
#if UNITY_EDITOR
        private void Awake() => Assert.IsNotNull(_target);
#endif
        
        public override void Run(int entity) => _target.enabled = _enabled;
    }
}