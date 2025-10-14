using Leopotam.EcsLite;

namespace Core.Lib
{
    public abstract class AbstractBaseProvider
    {
        public abstract void Attach(int entity, EcsWorld world);
        public abstract void Del(int entity, EcsWorld world);
        public abstract bool Has(int entity, EcsWorld world);
    }
}