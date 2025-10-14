using System.Runtime.CompilerServices;

namespace Core.Lib
{
    public static class IntStringCache
    {
        private static readonly Glossary<string> _cache = new(1024);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Get(int value)
        {
            if (_cache.Length > 10000)
                _cache.Clear();
            
            if (!_cache.TryGetValue(value, out var s))
                _cache.Add(value, s = value.ToString());

            return s;
        }
    }
}