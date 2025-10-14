using Core.Components;
using Lib;

namespace Core.ExternalEntityLogics
{
    public class ActionWaitStreamingCancelSubLogic : MonoConstruct, IButtonGenericLogic, IActionSubStartLogic, IActionSubCancelLogic
    {
        private LazyPool _waitStreamingCancelPool;
        private AbstractEntityAction _action;

        private void Awake()
        {
            _action = GetComponentInParent<AbstractEntityAction>();
            _waitStreamingCancelPool = new LazyPool(context);
        }

        public void SubStart(int entity) => _action.InvokeButtonContext(this, entity);

        public void GenericRun<T>(int entity) where T : struct, IButtonComponent =>
            _waitStreamingCancelPool.GetPool<WaitStreamingCancel<T>>().Add(entity);

        public void SubCancel(int entity) => _waitStreamingCancelPool.Del(entity);
    }
}