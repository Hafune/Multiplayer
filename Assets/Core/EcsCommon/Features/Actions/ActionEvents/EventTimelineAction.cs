using Core.Lib;
using Leopotam.EcsLite;

namespace Core.Components
{
    public struct EventTimelineAction : IEcsAutoReset<EventTimelineAction>
    {
        public ActionNonAlloc<int> logic;
        public void AutoReset(ref EventTimelineAction c)
        {
            c.logic ??= new();
            c.logic.Clear();
        }
    }
}