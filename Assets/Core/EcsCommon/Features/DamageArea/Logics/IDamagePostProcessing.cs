namespace Core.ExternalEntityLogics
{
    interface IDamagePostProcessing
    {
        public float PostProcessValue(int entity, float damage);
    }
}