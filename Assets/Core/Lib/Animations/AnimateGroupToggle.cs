using UnityEngine;

namespace Core.Lib
{
    public class AnimateGroupToggle : AbstractAnimateToggle
    {
        [SerializeField] private AbstractAnimateToggle[] _targets;

        public override void SetValue(bool value) => enabled = value;

        private void OnEnable()
        {
            for (int i = 0, iMax = _targets.Length; i < iMax; i++)
                _targets[i].SetValue(enabled);
        }

        private void OnDisable()
        {
            for (int i = 0, iMax = _targets.Length; i < iMax; i++)
                _targets[i].SetValue(enabled);
        }
    }
}