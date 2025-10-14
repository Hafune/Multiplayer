using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class SetupNpcActionLogic : AbstractEntityLogic
    {
        [SerializeField] private AbstractEntityActionStateful _action;
        private EcsPool<NpcActionComponent> _npcAction;

        private void Awake() => _npcAction = context.Resolve<ComponentPools>().NpcAction;

        public override void Run(int entity) => _npcAction.GetOrInitialize(entity).logic = _action;
    }
}