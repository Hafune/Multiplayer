using Core.Components;
using Core.Lib;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.ExternalEntityLogics
{
    public class SpawnEntityLogic : AbstractEntityLogic
    {
        [SerializeField] private ConvertToEntity _prefab;
        [SerializeField] private AbstractEntityLogic _childProcessing;
        private EntityBuilder _entityBuilder;

        private void Awake()
        {
            _entityBuilder = new EntityBuilder(context);
#if UNITY_EDITOR
            Assert.IsNotNull(_prefab);
#endif
        }

        public override void Run(int entity)
        {
            transform.GetPositionAndRotation(out var  position, out var rotation);
            var child = _entityBuilder.Build(_prefab, position, rotation, null, entity);
            _childProcessing?.Run(child);
        }
    }
}