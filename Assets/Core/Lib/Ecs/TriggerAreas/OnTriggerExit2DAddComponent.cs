using Leopotam.EcsLite;
using Lib;
using UnityEngine;
using Core.Lib;

public class OnTriggerExit2DAddComponent : MonoConstruct, ITriggerDispatcherTarget2D
{
    [SerializeField] private BaseMonoProvider _monoProvider;
    [SerializeField] private bool _removeWithEnter;

    private ConvertToEntity _entityRef;
    private EcsWorld _world;
    private int _contactCount;

    private void Awake()
    {
        _entityRef = GetComponentInParent<ConvertToEntity>();
        _world = context.Resolve<EcsWorld>();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (++_contactCount == 1 && _removeWithEnter && _entityRef.RawEntity != -1)
            _monoProvider.Del(_entityRef.RawEntity, _world);
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (--_contactCount == 0 && _entityRef.RawEntity != -1)
            _monoProvider.Attach(_entityRef.RawEntity, _world);
    }
}