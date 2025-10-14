using Core.Lib;
using Leopotam.EcsLite;

namespace Core.Components
{
    public struct EventEndFrameCall : IEcsAutoReset<EventEndFrameCall>
    {
        public ActionNonAlloc<int> call;
        public void AutoReset(ref EventEndFrameCall c)
        {
            c.call ??= new();
            c.call.Clear();
        }
    }
}