using Core.Lib;
using Leopotam.EcsLite;

namespace Core.Components
{
    public struct DeathCallbackComponent : IEcsAutoReset<DeathCallbackComponent>
    {
        public ActionNonAlloc<int> call;
        
        public void AutoReset(ref DeathCallbackComponent c)
        {
            c.call ??= new();
            c.call.Clear();
        }
    }
}