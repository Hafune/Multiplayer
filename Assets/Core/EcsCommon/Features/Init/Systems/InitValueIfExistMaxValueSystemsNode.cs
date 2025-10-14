using System.Collections.Generic;
using Core.Components;
using Leopotam.EcsLite;

namespace Core.Systems
{
    public static class InitValueIfExistMaxValueSystemsNode
    {
        public static IEnumerable<IEcsSystem> BuildSystems() =>
            new IEcsSystem[]
            {
                new InitValueByMaxValueSystem<HitPointValueComponent, HitPointMaxValueComponent>(),
                new InitValueByMaxValueSystem<ManaPointValueComponent, ManaPointMaxValueComponent>(),
            };
    }
}