using UnityEngine;
using UnityEngine.Assertions;

namespace Core.ExternalEntityLogics
{
    public class SetActiveLogic : AbstractEntityLogic
    {
        [SerializeField] private GameObject _target;
        [SerializeField] private bool _isActive;

#if UNITY_EDITOR
        private void Awake() => Assert.IsNotNull(_target);
#endif
        
        public override void Run(int entity) => _target.SetActive(_isActive);
    }
}