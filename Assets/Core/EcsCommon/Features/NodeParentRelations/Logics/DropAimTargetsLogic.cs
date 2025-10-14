using Core.Components;
using Core.Generated;
using Core.Systems;
using Leopotam.EcsLite;

namespace Core.ExternalEntityLogics
{
    public class DropAimTargetsLogic : AbstractEntityLogic
    {
        private RelationFunctions<AimComponent, TargetComponent> _relationFunctions;
        private EcsPool<DropTargetWhenSheDeathTag> _dropTargetWhenSheDeath;

        private void Awake()
        {
            _relationFunctions = new(context);
            _dropTargetWhenSheDeath = context.Resolve<ComponentPools>().DropTargetWhenSheDeath;
        }

        public override void Run(int entity) => _relationFunctions.DisconnectNodeChildren(entity, _dropTargetWhenSheDeath);
    }
}