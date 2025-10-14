using UnityEngine;
using UnityEngine.Assertions;

namespace Core.ExternalEntityLogics
{
    public class SetActiveToggleSubLogic : MonoBehaviour, IActionSubStartLogic, IActionSubCancelLogic
    {
        [SerializeField] private GameObject _target;
        [SerializeField] private bool _isActive;

#if UNITY_EDITOR
        private void Awake() => Assert.IsNotNull(_target);
#endif

        public void SubStart(int entity) => _target.SetActive(_isActive);

        public void SubCancel(int entity) => _target.SetActive(!_isActive);
    }
}