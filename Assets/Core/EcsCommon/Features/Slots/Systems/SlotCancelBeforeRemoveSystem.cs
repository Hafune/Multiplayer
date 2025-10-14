using Core.Components;
using Core.EcsCommon.ValueSlotComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Reflex;

namespace Core.Systems
{
    public class SlotCancelBeforeRemoveSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                SlotNodeComponent,
                EventRemoveEntity
            >> _nodeFilter;

        private readonly EcsFilterInject<
            Inc<
                SlotTargetComponent,
                EventRemoveEntity
            >> _targetFilter;

        private readonly EcsPoolInject<SlotNodeComponent> _nodePool;
        private readonly EcsPoolInject<EventRemoveEntity> _eventRemoveEntityPool;
        private readonly EcsPoolInject<SlotComponent> _slotPool;
        private readonly SlotFunctions _slotFunctions;

        public SlotCancelBeforeRemoveSystem(Context context) => _slotFunctions = new(context);

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _nodeFilter.Value)
            foreach (var child in _nodePool.Value.Get(i).children)
            {
                if (!_eventRemoveEntityPool.Value.Has(child))
                    _slotPool.Value.Get(child).vfxCancelLogic?.Run(i);
            }

            foreach (var i in _targetFilter.Value)
                _slotFunctions.RemoveSlot(i);
        }
    }
}