using Core.Components;
using Core.Lib;
using Core.Systems;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class AddChildToAimLogic : AbstractEntityLogic
    {
        [SerializeField] private ConvertToEntity _prefab;
        [SerializeField] private AbstractEntityLogic _childPostProcessing;
        private ConvertToEntity _convertToEntity;
        private RelationFunctions<AimComponent, TargetComponent> _aimRelationFunctions;
        private EntityBuilder _entityBuilder;

        private void Awake()
        {
            _convertToEntity = GetComponentInParent<ConvertToEntity>();
            _entityBuilder = new EntityBuilder(context);
            _aimRelationFunctions = new(context);
        }

        public override void Run(int aim)
        {
            int child = _entityBuilder.Build(_prefab, Vector3.zero, Quaternion.identity, null, _convertToEntity.RawEntity);
            _aimRelationFunctions.Connect(aim, child);
            _childPostProcessing?.Run(child);
        }
    }
}