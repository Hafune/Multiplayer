using UnityEngine;

namespace Core.Lib
{
    public class ScaleEffectController : MonoBehaviour
    {
        [SerializeField] private Transform[] _targets;

        private Vector3[] _originalScales;
        private float _scale;
        
        private void Awake()
        {
            _originalScales = new Vector3[_targets.Length];
            for (int i = 0; i < _targets.Length; i++)
                _originalScales[i] = _targets[i].localScale;
        }

        private void OnDisable()
        {
            for (int i = 0; i < _targets.Length; i++)
                _targets[i].localScale = _originalScales[i];
        }

        public void SetScale(float multiplier)
        {
            for (int i = 0; i < _targets.Length; i++)
                _targets[i].localScale = _originalScales[i] * multiplier;
        }
    }
}


