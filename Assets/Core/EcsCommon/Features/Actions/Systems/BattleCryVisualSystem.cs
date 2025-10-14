using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    // public class BattleCryVisualSystem : IEcsRunSystem
    // {
    //     private readonly EcsFilterInject<
    //         Inc<
    //             BattleCryVisualComponent,
    //             SlotTimerComponent<BattleCrySlotComponent>
    //         >,
    //         Exc<
    //             InProgressTag<BattleCryVisualComponent>
    //         >> _setupFilter;
    //     
    //     private readonly EcsFilterInject<
    //         Inc<
    //             BattleCryVisualComponent,
    //             InProgressTag<BattleCryVisualComponent>
    //         >,
    //         Exc<
    //             SlotTimerComponent<BattleCrySlotComponent>
    //         >> _cancelFilter;
    //
    //     private readonly ComponentPools _pools;
    //     private readonly EcsPoolInject<ActionCurrentComponent> _actionCurrentPool;
    //
    //     public void Run(IEcsSystems systems)
    //     {
    //         foreach (var i in _setupFilter.Value)
    //         {
    //             _pools.BattleCryVisual.Get(i).Setup?.Run(i);
    //             _pools.InProgressBattleCryVisual.Add(i);
    //         }
    //         
    //         foreach (var i in _cancelFilter.Value)
    //         {
    //             _pools.BattleCryVisual.Get(i).Cancel?.Run(i);
    //             _pools.InProgressBattleCryVisual.Del(i);
    //         }
    //     }
    // }
}