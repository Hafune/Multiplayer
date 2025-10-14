using UnityEngine;

namespace Core.Lib
{
    public static class EasingFunctions
    {
        public static float Linear(float t) => t;

        public static float EaseInSine(float t) => 1f - Mathf.Cos((t * Mathf.PI) / 2f);
        public static float EaseOutSine(float t) => Mathf.Sin((t * Mathf.PI) / 2f);
        public static float EaseInOutSine(float t) => -(Mathf.Cos(Mathf.PI * t) - 1f) / 2f;

        public static float EaseInQuad(float t) => t * t;
        public static float EaseOutQuad(float t) => t * (2f - t);
        public static float EaseInOutQuad(float t) => t < 0.5f ? 2f * t * t : -1f + (4f - 2f * t) * t;

        public static float EaseInCubic(float t) => t * t * t;
        public static float EaseOutCubic(float t) => 1f - Mathf.Pow(1f - t, 3);
        public static float EaseInOutCubic(float t) => t < 0.5f ? 4f * t * t * t : 1f - Mathf.Pow(-2f * t + 2f, 3) / 2f;

        public static float EaseInQuart(float t) => t * t * t * t;
        public static float EaseOutQuart(float t) => 1f - Mathf.Pow(1f - t, 4);
        public static float EaseInOutQuart(float t) => t < 0.5f ? 8f * t * t * t * t : 1f - Mathf.Pow(-2f * t + 2f, 4) / 2f;

        public static float EaseInQuint(float t) => t * t * t * t * t;
        public static float EaseOutQuint(float t) => 1f - Mathf.Pow(1f - t, 5);
        public static float EaseInOutQuint(float t) => t < 0.5f ? 16f * t * t * t * t * t : 1f - Mathf.Pow(-2f * t + 2f, 5) / 2f;

        public static float EaseInExpo(float t) => t == 0 ? 0 : Mathf.Pow(2f, 10f * t - 10f);
        public static float EaseOutExpo(float t) => t == 1 ? 1 : 1 - Mathf.Pow(2f, -10f * t);
        public static float EaseInOutExpo(float t) => t == 0 ? 0 : t == 1 ? 1 : t < 0.5f ? Mathf.Pow(2f, 20f * t - 10f) / 2f : (2f - Mathf.Pow(2f, -20f * t + 10f)) / 2f;

        public static float EaseInCirc(float t) => 1f - Mathf.Sqrt(1f - Mathf.Pow(t, 2));
        public static float EaseOutCirc(float t) => Mathf.Sqrt(1f - Mathf.Pow(t - 1f, 2));
        public static float EaseInOutCirc(float t) => t < 0.5f ? (1f - Mathf.Sqrt(1f - Mathf.Pow(2f * t, 2))) / 2f : (Mathf.Sqrt(1f - Mathf.Pow(-2f * t + 2f, 2)) + 1f) / 2f;

        public static float EaseInBack(float t, float c1 = 1.70158f) => (c1 + 1f) * t * t * t - c1 * t * t;
        public static float EaseOutBack(float t, float c1 = 1.70158f) => 1f + (c1 + 1f) * Mathf.Pow(t - 1f, 3) + c1 * Mathf.Pow(t - 1f, 2);
        public static float EaseInOutBack(float t, float c1 = 1.70158f) => t < 0.5f ? (Mathf.Pow(2f * t, 2) * ((c1 * 1.525f + 1f) * 2f * t - c1 * 1.525f)) / 2f : (Mathf.Pow(2f * t - 2f, 2) * ((c1 * 1.525f + 1f) * (t * 2f - 2f) + c1 * 1.525f) + 2f) / 2f;

        public static float EaseInElastic(float t) => t == 0 ? 0 : t == 1 ? 1 : -Mathf.Pow(2f, 10f * t - 10f) * Mathf.Sin((t * 10f - 10.75f) * (2f * Mathf.PI) / 3f);
        public static float EaseOutElastic(float t) => t == 0 ? 0 : t == 1 ? 1 : Mathf.Pow(2f, -10f * t) * Mathf.Sin((t * 10f - 0.75f) * (2f * Mathf.PI) / 3f) + 1f;
        public static float EaseInOutElastic(float t) => t == 0 ? 0 : t == 1 ? 1 : t < 0.5f ? -(Mathf.Pow(2f, 20f * t - 10f) * Mathf.Sin((20f * t - 11.125f) * (2f * Mathf.PI) / 4.5f)) / 2f : Mathf.Pow(2f, -20f * t + 10f) * Mathf.Sin((20f * t - 11.125f) * (2f * Mathf.PI) / 4.5f) / 2f + 1f;

        public static float EaseInBounce(float t) => 1f - EaseOutBounce(1f - t);
        public static float EaseOutBounce(float t)
        {
            const float n1 = 7.5625f;
            const float d1 = 2.75f;

            if (t < 1f / d1)
            {
                return n1 * t * t;
            }
            else if (t < 2f / d1)
            {
                return n1 * (t -= 1.5f / d1) * t + 0.75f;
            }
            else if (t < 2.5f / d1)
            {
                return n1 * (t -= 2.25f / d1) * t + 0.9375f;
            }
            else
            {
                return n1 * (t -= 2.625f / d1) * t + 0.984375f;
            }
        }
        public static float EaseInOutBounce(float t) => t < 0.5f ? (1f - EaseOutBounce(1f - 2f * t)) / 2f : (1f + EaseOutBounce(2f * t - 1f)) / 2f;
    }
}