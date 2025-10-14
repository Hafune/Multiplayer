using System;
using Lib;

namespace Core.Lib
{
    public class EffectNodeDynamic : AbstractEffect
    {
        private DynamicObjectTracker<AbstractEffect> _tracker;
        private Action<AbstractEffect> _forEachActive;

        private void Awake()
        {
            var effects = transform.GetSelfChildrenComponents<AbstractEffect>(true);
            _tracker = new DynamicObjectTracker<AbstractEffect>(effects);
            _forEachActive = ForEachActive;
        }

        public override void Execute() => _tracker.ForEachActive(_forEachActive);

        private static void ForEachActive(AbstractEffect effect) => effect.Execute();
    }
}