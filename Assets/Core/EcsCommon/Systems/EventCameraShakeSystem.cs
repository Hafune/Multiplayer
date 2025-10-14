// using Com.LuisPedroFonseca.ProCamera2D;
using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Reflex;

namespace Core.Systems
{
    public class EventCameraShakeSystem// : IEcsRunSystem
    {
        // private readonly EcsFilterInject<Inc<EventCameraShakeComponent>> _filter;
        //
        // private readonly EcsPoolInject<EventCameraShakeComponent> _pool;
        //
        // private readonly ProCamera2DShake _cameraShake;
        //
        // public EventCameraShakeSystem(Context context) => _cameraShake =
        //     context.Resolve<ProCamera2D>().GetComponent<ProCamera2DShake>();
        //
        // public void Run(IEcsSystems systems)
        // {
        //     foreach (var i in _filter.Value)
        //     {
        //         var shake = _pool.Value.Get(i);
        //         _cameraShake.Shake(shake.preset);
        //         _pool.Value.Del(i);
        //     }
        // }
    }
}