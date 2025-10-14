using Core.Components;
using Core.ExternalEntityLogics;
using Core.Lib;

namespace Core.Systems
{
    public class MoveSpeedPostProcessing : AbstractAnimationSpeedPostProcessing
    {
        private ValueListener<MoveSpeedValueComponent> _listener;

        private void Awake()
        {
            var entityRef = GetComponentInParent<ConvertToEntity>();
            _listener = new(entityRef, _ => OnChange?.Invoke(entityRef.RawEntity));
        }

        public override float CalculateValue(int entity, float speed) => speed + _listener.Get().value;
    }
}