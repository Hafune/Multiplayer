using System.Collections.Generic;
using Core.Components;
using Leopotam.EcsLite;

namespace Core.Systems
{
    public static class InitEquateValuesSystemsNode
    {
        public static IEnumerable<IEcsSystem> BuildSystems() => new IEcsSystem[]
        {
            //Установка макс значений при инициализации
            new InitEquateValueToMaxValueSystem<HitPointValueComponent, HitPointMaxValueComponent>(),
            // new InitEquateValueToMaxValueSystem<ManaPointValueComponent, ManaPointMaxValueComponent>(),
        };
    }
}