using System.Runtime.CompilerServices;
using Core.Components;
using Core.EcsCommon.ValueSlotComponents;
using Core.Generated;
using Core.Lib;
using Leopotam.EcsLite;
using Lib;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Core.ExternalEntityLogics
{
    [DisallowMultipleComponent]
    public class SlotWithTimerLogic : AbstractEntityLogic
    {
        [SerializeField] private SlotTimerData value;
        private EcsPool<SlotTimersCooldownComponent> _cooldownPool;
        private EcsPool<RemoveTimerComponent> _removeTimerPool;
        private SlotFunctions _slotFunctions;

        private void Awake()
        {
            var pools = context.Resolve<ComponentPools>();
            _removeTimerPool = pools.RemoveTimer;
            _cooldownPool = pools.SlotTimersCooldown;
            _slotFunctions = new SlotFunctions(context);
        }

        public override void Run(int entity)
        {
            if (!CheckCooldown(entity))
                return;

            int child = _slotFunctions.AddSlot(entity, value.value);

            ref var timer = ref _removeTimerPool.GetOrInitialize(child);
            timer.duration = value.duration;
            timer.startTime = Time.time;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool CheckCooldown(int entity)
        {
            var eventSlotTimer = value;
            var id = SlotUtility.GetIdByKey(eventSlotTimer.value.key);

            if (eventSlotTimer.cooldown == 0 || id == 0)
                return true;

            var cooldownComponent = _cooldownPool.Get(entity);
            var index = cooldownComponent.ids.IndexOf(id);

            var currentTime = Time.time;
            if (index >= 0)
            {
                if (currentTime - cooldownComponent.times[index] < eventSlotTimer.cooldown)
                    return false;

                cooldownComponent.times[index] = currentTime;
            }
            else
            {
                cooldownComponent.ids.Add(id);
                cooldownComponent.times.Add(currentTime);
            }

            return true;
        }

#if UNITY_EDITOR
        [MyButton]
        private void SetPathAsKey()
        {
            var slotComponent = value.value;
            slotComponent.key = transform.root.name + "/" + gameObject.GetPath();
            value.value = slotComponent;
            EditorUtility.SetDirty(this);
        }
#endif
    }
}