namespace Core.Components
{
    public struct DamagePerSecondComponent
    {
        public float duration;
        public float damagePerSecond;
        public float startTime;
        public float lastTickTime;
        public float tickDelay;
        public bool showDamage;
    }
}