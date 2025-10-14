using System.Collections.Generic;
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;

namespace Core.Systems
{
    public static class RecalculateValueSystemsNode
    {
        public static IEnumerable<IEcsSystem> BuildSystems() => new IEcsSystem[]
        {
            new InitRecalculateAllValuesSystem(),
            new EventStartRecalculateValueSystem(),
            new RemoveEventUpdatedSlotTagSystem(),
        };
    }
}