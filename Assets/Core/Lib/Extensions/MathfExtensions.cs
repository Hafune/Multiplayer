using System.Runtime.CompilerServices;
using UnityEngine;

namespace Core.Lib
{
    public static class MathfExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SmoothDampWithOvershoot(
            float currentVelocity,
            float target,
            ref float velocity,
            float smoothTime)
        {
            float deltaTime = Time.deltaTime;
            smoothTime = Mathf.Max(0.0001f, smoothTime);
            float num1 = 2f / smoothTime;
            float num2 = num1 * deltaTime;
            float num3 = (float)(1.0 / (1.0 + (double)num2 + 0.479999989271164 * (double)num2 * (double)num2 +
                                        0.234999999403954 * (double)num2 * (double)num2 * (double)num2));
            float num4 = currentVelocity - target;
            float max = float.PositiveInfinity * smoothTime;
            float num6 = Mathf.Clamp(num4, -max, max);
            target = currentVelocity - num6;
            float num7 = (velocity + num1 * num6) * deltaTime;
            velocity = (velocity - num1 * num7) * num3;
            float num8 = target + (num6 + num7) * num3;

            return num8;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SmoothDampAngleWithOvershoot(
            float current,
            float target,
            ref float currentVelocity,
            float smoothTime)
        {
            target = current + Mathf.DeltaAngle(current, target);
            float deltaTime = Time.deltaTime;
            smoothTime = Mathf.Max(0.0001f, smoothTime);
            float num1 = 2f / smoothTime;
            float num2 = num1 * deltaTime;
            float num3 = (float)(1.0 / (1.0 + (double)num2 + 0.479999989271164 * (double)num2 * (double)num2 +
                                        0.234999999403954 * (double)num2 * (double)num2 * (double)num2));
            float num4 = current - target;
            float max = float.PositiveInfinity * smoothTime;
            float num6 = Mathf.Clamp(num4, -max, max);
            target = current - num6;
            float num7 = (currentVelocity + num1 * num6) * deltaTime;
            currentVelocity = (currentVelocity - num1 * num7) * num3;
            float num8 = target + (num6 + num7) * num3;
            return num8;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 SmoothDampWithOvershoot(
            Vector3 current,
            Vector3 target,
            ref Vector3 currentVelocity,
            float smoothTime)
        {
            float deltaTime = Time.deltaTime;

            smoothTime = Mathf.Max(0.0001f, smoothTime);
            float num1 = 2f / smoothTime;
            float num2 = num1 * deltaTime;
            float num3 = (float)(1.0 / (1.0 + (double)num2 + 0.479999989271164 * (double)num2 * (double)num2 +
                                        0.234999999403954 * (double)num2 * (double)num2 * (double)num2));
            float num4 = current.x - target.x;
            float num5 = current.y - target.y;
            float num6 = current.z - target.z;

            target.x = current.x - num4;
            target.y = current.y - num5;
            target.z = current.z - num6;
            float num10 = (currentVelocity.x + num1 * num4) * deltaTime;
            float num11 = (currentVelocity.y + num1 * num5) * deltaTime;
            float num12 = (currentVelocity.z + num1 * num6) * deltaTime;
            currentVelocity.x = (currentVelocity.x - num1 * num10) * num3;
            currentVelocity.y = (currentVelocity.y - num1 * num11) * num3;
            currentVelocity.z = (currentVelocity.z - num1 * num12) * num3;
            float x = target.x + (num4 + num10) * num3;
            float y = target.y + (num5 + num11) * num3;
            float z = target.z + (num6 + num12) * num3;

            return new Vector3(x, y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 SmoothDampWithOvershoot(
            Vector2 current,
            Vector2 target,
            ref Vector2 currentVelocity,
            float smoothTime)
        {
            float deltaTime = Time.deltaTime;

            smoothTime = Mathf.Max(0.0001f, smoothTime);
            float num1 = 2f / smoothTime;
            float num2 = num1 * deltaTime;
            float num3 = (float)(1.0 / (1.0 + (double)num2 + 0.479999989271164 * (double)num2 * (double)num2 +
                                        0.234999999403954 * (double)num2 * (double)num2 * (double)num2));
            float num4 = current.x - target.x;
            float num5 = current.y - target.y;

            target.x = current.x - num4;
            target.y = current.y - num5;
            float num10 = (currentVelocity.x + num1 * num4) * deltaTime;
            float num11 = (currentVelocity.y + num1 * num5) * deltaTime;
            currentVelocity.x = (currentVelocity.x - num1 * num10) * num3;
            currentVelocity.y = (currentVelocity.y - num1 * num11) * num3;
            float x = target.x + (num4 + num10) * num3;
            float y = target.y + (num5 + num11) * num3;

            return new Vector2(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion SmoothDampWithOvershoot(
            Quaternion current,
            Quaternion target,
            ref Vector3 currentVelocity,
            float smoothTime)
        {
            var deltaRotation = Quaternion.Inverse(target) * current;
            var targetAngles = target.eulerAngles;
            var deltaAngles = deltaRotation.eulerAngles;

            var rotationX = GetAxisRotation(ref currentVelocity.x, Vector3.right, deltaAngles.x, targetAngles.x, smoothTime);
            var rotationY = GetAxisRotation(ref currentVelocity.y, Vector3.up, deltaAngles.y, targetAngles.y, smoothTime);
            var rotationZ = GetAxisRotation(ref currentVelocity.z, Vector3.forward, deltaAngles.z, targetAngles.z, smoothTime);

            return rotationX * rotationY * rotationZ;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Quaternion GetAxisRotation(
            ref float _angleVelocity,
            Vector3 direction,
            float delta,
            float targetAngle,
            float smooth)
        {
            delta = Mathf.DeltaAngle(0, delta);

            // Плавное изменение угла с помощью расширенного метода с overshoot
            float value = SmoothDampAngleWithOvershoot(delta, targetAngle, ref _angleVelocity, smooth);

            // Вычисляем вращение для смещения на основе разницы между углом текущим и сглаженным
            return Quaternion.AngleAxis(value - delta, direction);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 SmoothStep(Vector3 from, Vector3 to, float t)
        {
            t = Mathf.Clamp01(t); // Ensure t is between 0 and 1
            t = Mathf.SmoothStep(0f, 1f, t); // Apply smooth step to t

            // Interpolate each component
            return new Vector3(
                Mathf.Lerp(from.x, to.x, t),
                Mathf.Lerp(from.y, to.y, t),
                Mathf.Lerp(from.z, to.z, t)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 SmoothStep(Vector2 from, Vector2 to, float t)
        {
            t = Mathf.Clamp01(t); // Ensure t is between 0 and 1
            t = Mathf.SmoothStep(0f, 1f, t); // Apply smooth step to t

            // Interpolate each component
            return new Vector2(
                Mathf.Lerp(from.x, to.x, t),
                Mathf.Lerp(from.y, to.y, t)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ClampMagnitudeMin(Vector2 vector, float minLength)
        {
            float sqrMagnitude = vector.sqrMagnitude;
            if ((double) sqrMagnitude >= (double) minLength * (double) minLength)
                return vector;
            
            return vector.normalized * minLength;
        }
    }
}