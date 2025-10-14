using UnityEngine;

namespace Core.Lib
{
    public class SyncActiveState : MonoBehaviour
    {
        [SerializeField] private GameObject _target;
        [SerializeField] private bool _active = true;

        private void OnEnable()
        {
            if (_target)
                _target.SetActive(_active);
        }

        private void OnDisable()
        {
            if (_target)
                _target.SetActive(!_active);
        }
    }
}