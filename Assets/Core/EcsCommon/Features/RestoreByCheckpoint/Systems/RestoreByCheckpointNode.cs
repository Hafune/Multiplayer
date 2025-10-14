using Core.Components;
using Leopotam.EcsLite;

namespace Core.Systems
{
    public static class RestoreByCheckpointNode
    {
        public static IEcsSystem[] BuildSystems()
        {
            return new IEcsSystem[]
            {
                new RestoreByCheckpointSystem<HitPointValueComponent, HitPointMaxValueComponent>(),
                //
                new DelHere<EventRestoreConsumables>(),
            };
        }
    }
}