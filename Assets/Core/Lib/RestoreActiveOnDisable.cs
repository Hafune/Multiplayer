using UnityEngine;

namespace Core.Lib
{
    public class RestoreActiveOnDisable : MonoBehaviour
    {
        [SerializeField] private GameObject _target;
        private bool _active;

        private void Awake() => _active = _target.activeSelf;

        private void OnDisable() => _target.SetActive(_active);
    }
}