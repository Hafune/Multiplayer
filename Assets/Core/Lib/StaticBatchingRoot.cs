using Core.Lib.Utils;
using Lib;

namespace Core.Lib
{
    public class StaticBatchingRoot : MonoConstruct
    {
        private void Start() => MyStaticBatchingUtility.Combine(gameObject);
    }
}