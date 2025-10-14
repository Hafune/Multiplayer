using UnityEngine;

namespace Core.Lib
{
    public class RestoreScaleOnDisable : MonoBehaviour
    {
        private Vector3 _scale;

        private void Awake() => _scale = transform.localScale;

        private void OnDisable() => transform.localScale = _scale;
    }
}