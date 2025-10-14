using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using UnityEngine;

namespace Core.Systems
{
    public class CompleteIfResourceIsOverSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                ActionCostPerSecondValueComponent,
                EventValueUpdated<ActionCostPerSecondValueComponent>,
                ManaPointValueComponent
            >,
            Exc<
                InProgressTag<CompleteIfResourceIsOverSystem>
            >> _startFilter;

        private readonly EcsFilterInject<
            Inc<
                ActionCostPerSecondValueComponent,
                InProgressTag<CompleteIfResourceIsOverSystem>,
                ManaPointValueComponent
            >> _progressFilter;

        private readonly EcsPoolInject<InProgressTag<CompleteIfResourceIsOverSystem>> _progressPool;
        private readonly EcsPoolInject<ActionCostPerSecondValueComponent> _perSecondPool;
        private readonly EcsPoolInject<ManaPointValueComponent> _resourcePool;
        private readonly EcsPoolInject<ActionCompleteTag> _actionCompletePool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _startFilter.Value)
            {
                if (_perSecondPool.Value.Get(i).value == 0)
                    continue;

                _progressPool.Value.Add(i);
            }

            foreach (var i in _progressFilter.Value)
            {
                var value = _perSecondPool.Value.Get(i).value;
                
                if (_resourcePool.Value.Get(i).value + value * Time.deltaTime <= 0)
                {
                    _progressPool.Value.Del(i);
                    _actionCompletePool.Value.AddIfNotExist(i);
                }
                else if (value == 0)
                {
                    _progressPool.Value.Del(i);
                }
            }
        }
    }
}