using UnityEngine;

namespace Core.Lib
{
    public class RestoreRotationOnDisable : MonoBehaviour
    {
        private Quaternion _rotation;

        private void Awake() => _rotation = transform.localRotation;

        private void OnDisable() => transform.localRotation = _rotation;
    }
}