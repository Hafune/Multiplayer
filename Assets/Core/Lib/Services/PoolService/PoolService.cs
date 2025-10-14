using Reflex;
using UnityEngine;

namespace Core.Lib.Services
{
    public class PoolService : IInitializableService
    {
        private EcsEngine _ecsEngine;
        public MyPool ScenePool { get; private set; }
        public MyPool DontDisposablePool { get; private set; }

        public void InitializeService(Context context)
        {
            ScenePool = new MyPool(context);
            DontDisposablePool = new MyPool(context,  true);

            context.Resolve<AddressableService>().OnClearPools += ClearPools;
            _ecsEngine = context.Resolve<EcsEngine>();
        }
        
        public PrefabPool<T> GetIsolatedPullByPrefab<T>(T prefab) where T : Component => 
            DontDisposablePool.GetIsolatedPullByPrefab(prefab);

        private void ClearPools()
        {
            ScenePool.ForceDisable();
            DontDisposablePool.ReturnDisabledInContainer();
            _ecsEngine.Tick();
            TriggerCache.Cache.Clear();
            ScenePool.Clear();
        }
    }
}