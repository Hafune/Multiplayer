using Leopotam.EcsLite;

namespace Core.Components
{
    public struct EventCausedDamage : IEcsAutoReset<EventCausedDamage>
    {
        public float[] damages;
        public int damagesCount;

        public void AutoReset(ref EventCausedDamage c)
        {
            c.damages ??= new float[1];
            c.damagesCount = 0;
        }
    }
}