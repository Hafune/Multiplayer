#if UNITY_EDITOR
#endif

namespace Core.ExternalEntityLogics
{
    public abstract class AbstractEntityActionStateful : AbstractEntityAction
    {
        public abstract void StartLogic(int entity);

        public abstract void UpdateLogic(int entity);

        public abstract void CompleteStreamingLogic(int entity);

        public abstract void CancelLogic(int entity);
    }
}