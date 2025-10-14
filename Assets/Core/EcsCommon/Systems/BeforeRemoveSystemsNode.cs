using System.Collections.Generic;
using Core.Components;
using Leopotam.EcsLite;

namespace Core.Systems
{
    //Необходимость системы под вопросом
    public static class BeforeRemoveSystemsNode
    {
        public static IEnumerable<IEcsSystem> BuildSystems() => new IEcsSystem[]
        {
            new AuraAreaBeforeRemoveSystem(),
        };
    }
}