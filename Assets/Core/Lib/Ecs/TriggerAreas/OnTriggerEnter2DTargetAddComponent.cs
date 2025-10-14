using Leopotam.EcsLite;
using Lib;
using UnityEngine;

namespace Core.Lib
{
    public class OnTriggerEnter2DTargetAddComponent : MonoConstruct, ITriggerDispatcherTarget2D
    {
        [SerializeField] private BaseMonoProvider _monoProvider;
        [SerializeField] private bool _removeWithExit;

        private EcsWorld _world;

        private void Awake() => _world = context.Resolve<EcsWorld>();

        public void OnTriggerEnter2D(Collider2D col)
        {
            var entityRef = TriggerCache.ExtractEntity(col);
            _monoProvider.Attach(entityRef.RawEntity, _world);
        }

        public void OnTriggerExit2D(Collider2D col)
        {
            if (!_removeWithExit)
                return;

            var entityRef = TriggerCache.ExtractEntity(col);
            _monoProvider.Del(entityRef.RawEntity, _world);
        }
    }
}