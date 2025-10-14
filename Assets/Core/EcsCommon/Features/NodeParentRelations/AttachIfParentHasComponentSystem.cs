using System;
using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using Reflex;

namespace Core.Systems
{
    public class AttachIfParentHasComponentSystem<T, V> : IEcsRunSystem
        where T : struct
        where V : struct
    {
        private readonly EcsFilterInject<
            Inc<
                T,
                NodeComponent
            >> _filter;

        private readonly EcsPoolInject<ListenParentTag<V>> _providerPool;
        private readonly EcsPoolInject<V> _valuePool;

        private readonly RelationFunctions<NodeComponent, ParentComponent> _relationFunctions;

        public AttachIfParentHasComponentSystem(Context context) => _relationFunctions = new(context);

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            foreach (var e in _relationFunctions.EnumerateSelfChilds(i, _providerPool.Value))
                AddParentValueTag(e);
        }

        private void AddParentValueTag(int entity) => _valuePool.Value.AddIfNotExist(entity);
    }
}