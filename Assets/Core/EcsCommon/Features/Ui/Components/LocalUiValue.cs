using Core.Lib;
using Leopotam.EcsLite;

namespace Core.Components
{
    public struct LocalUiValue<T> : IEcsAutoReset<LocalUiValue<T>> where T : struct
    {
        public ActionNonAlloc<T> update;

        public void AutoReset(ref LocalUiValue<T> c)
        {
            c.update ??= new();
            c.update.Clear();
        }
    }
}