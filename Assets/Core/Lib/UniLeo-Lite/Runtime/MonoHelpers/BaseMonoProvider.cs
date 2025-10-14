using Leopotam.EcsLite;
using UnityEngine;

namespace Core.Lib
{
    public abstract class BaseMonoProvider : MonoBehaviour
    {
        public abstract void Attach(int entity, EcsWorld world);
        public abstract void Del(int entity, EcsWorld world);
        public abstract bool Has(int entity, EcsWorld world);
    }
}