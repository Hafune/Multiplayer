using Core.Components;
using Core.EcsCommon.ValueSlotComponents;
using Core.Generated;
using Core.Lib;
using Core.Systems;
using Leopotam.EcsLite;
using Lib;
using UnityEditor;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    [DisallowMultipleComponent]
    public class DependentSlotToParentLogic : AbstractEntityLogic
    {
        [SerializeField] public SlotComponent value;
        private SlotFunctions _slotFunctions;
        private EcsPool<ParentComponent> _parentPool;
        private EcsPool<RemoveWithParentTag> _removeWithParentPool;
        private RelationFunctions<NodeComponent, ParentComponent> _relationFunctions;

        private void Awake()
        {
            _relationFunctions = new(context);
            _slotFunctions = new SlotFunctions(context);
            var pools = context.Resolve<ComponentPools>();
            _parentPool = pools.Parent;
            _removeWithParentPool = pools.RemoveWithParent;
        }

        public override void Run(int entity)
        {
            var child = _slotFunctions.AddSlot(_parentPool.Get(entity).entity, value);
            _relationFunctions.Connect(entity, child);
            _removeWithParentPool.Add(child);
        }

#if UNITY_EDITOR
        [MyButton]
        private void SetPathAsKey()
        {
            value.key = transform.root.name + "/" + gameObject.GetPath();
            EditorUtility.SetDirty(this);
        }
#endif
    }
}