using System;
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    [Obsolete("Использование под вопросом, пока логичнее передавать компонент прямо в месте получения")]
    public class CopyToParentSystem<T> : IEcsRunSystem
        where T : struct
    {
        private EcsWorldInject _world;
        
        private readonly EcsFilterInject<
            Inc<
                T,
                ParentComponent,
                CopyToParentTag<T>
            >> _prepareFilter;

        private readonly EcsFilterInject<
            Inc<
                T,
                EventCopy
            >> _transportFilter;

        private readonly EcsPoolInject<T> _pool;
        private readonly EcsPoolInject<EventCopy> _eventPool;
        private readonly ComponentPools _pools; 

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _prepareFilter.Value)
            {
                int transport = _world.Value.NewEntity();
                _eventPool.Value.Add(transport).entity = _pools.Parent.Get(i).entity;
                _pool.Value.Copy(i, transport);
                _pool.Value.Del(i);
            }

            foreach (var i in _transportFilter.Value)
            {
                _pool.Value.Copy(i, _eventPool.Value.Get(i).entity);
                _world.Value.DelEntity(i);
            }
        }
    }
}