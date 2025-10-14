using System.Threading;

namespace Core.Lib
{
    public static class TypeIdCounter
    {
        public static int _nextId = 0;
    }

    public static class TypeId<T>
    {
        public static readonly int Id;

        static TypeId() => Id = Interlocked.Increment(ref TypeIdCounter._nextId) - 1;
    }
}