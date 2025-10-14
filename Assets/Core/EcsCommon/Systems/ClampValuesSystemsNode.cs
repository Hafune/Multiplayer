using System.Collections.Generic;
using Core.Components;
using Leopotam.EcsLite;

namespace Core.Systems
{
    public static class ClampValuesSystemsNode
    {
        public static IEnumerable<IEcsSystem> BuildSystems() => new IEcsSystem[]
        {
            //Установка макс значений при инициализации
            new ClampValueByMaxValueSystem<HitPointValueComponent, HitPointMaxValueComponent>(),
            new ClampValueByMaxValueSystem<ManaPointValueComponent, ManaPointMaxValueComponent>(),
        };
    }
}