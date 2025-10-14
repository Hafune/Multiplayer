using UnityEngine;

namespace Core.Lib
{
    public class GroupFloatScale : AbstractAnimateFloat
    {
        [SerializeField] public float scale;
        [SerializeField] private AbstractAnimateFloat[] _targets;

        public override void SetValue(float value)
        {
            for (int i = 0, iMax = _targets.Length; i < iMax; i++)
                _targets[i].SetValue(value * scale);
        }
    }
}