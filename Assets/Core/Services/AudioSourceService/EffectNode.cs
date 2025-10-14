using System.Linq;
using Lib;

namespace Core.Lib
{
    public class EffectNode : AbstractEffect
    {
        private AbstractEffect[] _effects;

        private void Awake()
        {
            _effects = transform
                .GetSelfChildrenComponents<AbstractEffect>(true)
                .ToArray();
        }

        public override void Execute()
        {
            for (int i = 0, iMax = _effects.Length; i < iMax; i++)
                _effects[i].Execute();
        }
    }
}