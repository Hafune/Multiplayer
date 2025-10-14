using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Lib
{
    public static class MyVectorExtensions
    {
        public static Vector3 Divide(this Vector3 v1, Vector3 v2) => new(v1.x / v2.x, v1.y / v2.y, v1.z / v2.z);

        public static Vector2 Divide(this Vector2 v1, Vector2 v2) => new(v1.x / v2.x, v1.y / v2.y);

        public static float ModuleDifference(this Vector3 v0, Vector3 v1)
        {
            var diff = v0 - v1;
            return Mathf.Abs(diff.x) + Mathf.Abs(diff.y) + Mathf.Abs(diff.z);
        }

        public static Vector3 Copy(this Vector3 vector, float? x = null, float? y = null, float? z = null) =>
            new(
                x ?? vector.x,
                y ?? vector.y,
                z ?? vector.z
            );

        public static Vector2 ToVector2XZ(this Vector3 vector) => new(vector.x, vector.z);
        public static Vector3 ToVector3XZ(this Vector2 vector) => new(vector.x, 0, vector.y);
        public static Vector3 ToVector3XZ(this Vector2Int vector) => new(vector.x, 0, vector.y);
        public static Vector3 ToVector3(this Vector2Int vector) => new(vector.x, vector.y, 0);

        public static Vector2 Copy(this Vector2 vector, float? x = null, float? y = null) =>
            new(
                x ?? vector.x,
                y ?? vector.y
            );

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 RotatedBy(this Vector2 v, float angle)
        {
            angle *= Mathf.Deg2Rad;
            var ca = Math.Cos(angle);
            var sa = Math.Sin(angle);
            var rx = v.x * ca - v.y * sa;

            return new Vector2((float)rx, (float)(v.x * sa + v.y * ca));
        }

        public static Vector2 RotatedBySignedAngle(this Vector2 v, Vector2 first, Vector2 second) => v.RotatedBy(
            Vector2.SignedAngle(first, second));

        public static Vector2 ReflectedBy(this Vector2 vector, Vector2 normal)
        {
            var reflect = Vector2.Reflect(vector.normalized, normal);
            return vector.RotatedBy(Vector2.SignedAngle(vector.normalized, reflect));
        }

        public static Vector2 RotatedToward(this Vector2 from, Vector2 to, float maxStep)
        {
            var angle = Vector2.SignedAngle(from, to);
            maxStep = Mathf.Min(Mathf.Abs(angle), maxStep) * maxStep.Sign();
            return angle is > 0 and < 180 ? from.RotatedBy(maxStep) : from.RotatedBy(-maxStep);
        }

        public static Vector2 ReflectedAlong(this Vector2 vector, Vector2 normal) =>
            (vector + vector.ReflectedBy(normal)) / 2;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion EulerZ(this Vector2 from, Vector2 to) =>
            Quaternion.Euler(0, 0, Vector2.SignedAngle(from, to));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Project(this Vector2 vector, Vector2 onNormal)
        {
            float num1 = Vector2.Dot(onNormal, onNormal);
            if (num1 < (double)Mathf.Epsilon)
                return Vector2.zero;
//(float) ((double) lhs.x * (double) rhs.x + (double) lhs.y * (double) rhs.y);
            float num2 = Vector2.Dot(vector, onNormal);
            return new Vector2(onNormal.x * num2 / num1, onNormal.y * num2 / num1);
        }
    }
}