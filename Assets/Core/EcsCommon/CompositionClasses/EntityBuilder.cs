using Core.Generated;
using Core.Lib;
using Core.Lib.Services;
using Core.Systems;
using Reflex;
using UnityEngine;

namespace Core.Components
{
    public class EntityBuilder
    {
        private readonly RelationFunctions<NodeComponent, ParentComponent> _relationFunctions;
        private readonly ComponentPools _pools;
        private readonly MyPool _scenePool;
        private readonly MyPool _dontDisposablePool;

        public EntityBuilder(Context context)
        {
            _relationFunctions = new(context);
            _pools = context.Resolve<ComponentPools>();
            var poolsService = context.Resolve<PoolService>();
            _scenePool = poolsService.ScenePool;
            _dontDisposablePool = poolsService.DontDisposablePool;
        }

        public int Build(ConvertToEntity prefab, Vector3 position, Quaternion rotation, Transform container, int parent)
        {
            var isDontDisposable = !_pools.Position.Has(parent) || _pools.Position.Get(parent).transform.gameObject.scene.buildIndex == -1;
            var convert = isDontDisposable
                ? _dontDisposablePool.GetInstanceByPrefab(prefab, position, rotation, container)
                : _scenePool.GetInstanceByPrefab(prefab, position, rotation, container);

            _relationFunctions.Connect(parent, convert.RawEntity);
            return convert.RawEntity;
        }
    }
}