using Core.Components;
using Core.Generated;
using Core.Systems;
using Leopotam.EcsLite;
using Lib;
using Reflex;
using Unity.Mathematics;

namespace Core.EcsCommon.ValueSlotComponents
{
    public readonly struct SlotFunctions
    {
        private readonly ComponentPools _pools;
        private readonly EcsWorld _world;
        private readonly RelationFunctions<SlotNodeComponent, SlotTargetComponent> _relationFunctions;

        public SlotFunctions(Context context)
        {
            _pools = context.Resolve<ComponentPools>();
            _world = context.Resolve<EcsWorld>();
            _relationFunctions = new(context);
        }

        public int AddSlot(int i, SlotComponent slot)
        {
            var id = SlotUtility.GetIdByKey(slot.key);
            var slotNode = _pools.SlotNode.GetOrInitialize(i);
            int index = -1;
            if (id != 0)
            {
                for (int c = 0, iMax = slotNode.children.Count; c < iMax; c++)
                {
                    if (id != _pools.Slot.Get(slotNode.children.Get(c)).id)
                        continue;

                    index = c;
                    break;
                }
            }

            int entity;
            if (index == -1)
            {
                entity = _world.NewEntity();
                _pools.RemoveWithSlotTarget.Add(entity);
                _relationFunctions.Connect(i, entity);
            }
            else
            {
                entity = slotNode.children[index];
            }

            ref var currentSlot = ref _pools.Slot.GetOrInitialize(entity);
            currentSlot.vfxCancelLogic?.Run(i);
            //Подсчёт стаков здесь это временное решение пока не потребуется другое более гибков для работы со стаками.
            currentSlot.currentStackCount = math.max(1, math.min(currentSlot.currentStackCount + 1, slot.maxStackCount));
            currentSlot.key = slot.key;
            currentSlot.id = id;

            currentSlot.values.Clear();

            if (currentSlot.currentStackCount == 1)
                currentSlot.values.AddRange(slot.values);
            else
                foreach (var data in slot.values)
                {
                    var valueData = data;
                    valueData.value *= currentSlot.currentStackCount;
                    currentSlot.values.Add(valueData);
                }

            currentSlot.tags.Clear();
            currentSlot.tags.AddRange(slot.tags);
            currentSlot.vfxStartLogic = slot.vfxStartLogic;
            currentSlot.vfxCancelLogic = slot.vfxCancelLogic;
            currentSlot.vfxStartLogic?.Run(i);

            _pools.EventStartRecalculateAllValues.AddIfNotExist(i);
            return entity;
        }

        public void RemoveSlot(int slotEntity)
        {
            _pools.EventRemoveEntity.AddIfNotExist(slotEntity);
            int parent = _pools.SlotTarget.Get(slotEntity).entity;
            _pools.Slot.Get(slotEntity).vfxCancelLogic?.Run(parent);
            _pools.EventStartRecalculateAllValues.AddIfNotExist(parent);
            _relationFunctions.DisconnectChild(slotEntity);
        }
    }
}