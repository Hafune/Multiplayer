using Leopotam.EcsLite;
using Lib;
using UnityEngine;

namespace Core.Lib
{
    public class OnTriggerEnterAddComponent : MonoConstruct, ITriggerDispatcherTarget
    {
        [SerializeField] private BaseMonoProvider _monoProvider;
        [SerializeField] private bool _removeWithExit;

        private ConvertToEntity _entityRef;
        private EcsWorld _world;
        private int _contactCount;

        private void Awake()
        {
            _entityRef = GetComponentInParent<ConvertToEntity>();
            _world = context.Resolve<EcsWorld>();
        }

        public void OnTriggerEnter(Collider col)
        {
            TriggerDisableHandler.RegisterTrigger(this, col);
            if (++_contactCount == 1 && _entityRef.RawEntity != -1)
                _monoProvider.Attach(_entityRef.RawEntity, _world);
        }

        public void OnTriggerExit(Collider col)
        {
            TriggerDisableHandler.UnRegisterTrigger(this, col);
            if (--_contactCount == 0 && _removeWithExit && _entityRef.RawEntity != -1)
                _monoProvider.Del(_entityRef.RawEntity, _world);
        }

    }
}