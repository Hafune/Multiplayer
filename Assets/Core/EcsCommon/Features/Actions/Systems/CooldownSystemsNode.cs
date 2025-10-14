using Core.Components;
using Leopotam.EcsLite;

namespace Core.Systems
{
    public static class CooldownSystemsNode
    {
        public static IEcsSystem[] BuildSystems()
        {
            return new IEcsSystem[]
            {
                new CooldownSystem<ActionLinkButton1Component>(),
                new CooldownSystem<ActionLinkButton2Component>(),
                new CooldownSystem<ActionLinkButton3Component>(),
                new CooldownSystem<ActionLinkButton4Component>(),
                new CooldownSystem<ActionLinkMouseLeftComponent>(),
                new CooldownSystem<ActionLinkMouseRightComponent>(),
                //
                new CooldownSystem<HealingPotionValueComponent>()
            };
        }
    }
}