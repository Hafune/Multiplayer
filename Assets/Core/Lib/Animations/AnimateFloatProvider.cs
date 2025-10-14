using UnityEngine;
using UnityEngine.Animations;

namespace Core.Lib
{
    public class AnimateFloatProvider : MonoBehaviour
    {
        [SerializeField] public Axis axis;
        [SerializeField] private AbstractVelocityInfo _info;
        [SerializeField] private float _valueScale = 1f;
        [SerializeField] private AbstractAnimateFloat[] _targets;

        private void FixedUpdate()
        {
            for (int i = 0, iMax = _targets.Length; i < iMax; i++)
                _targets[i].SetValue(_info.GetVelocity(axis) * _valueScale);
        }
    }
}