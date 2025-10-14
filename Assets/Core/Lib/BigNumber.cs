using UnityEngine;

namespace Core.Lib
{
    public class BigNumber
    {
        public float Mantissa; // Мантисса
        public int Exponent;   // Экспонента

        public BigNumber(float mantissa, int exponent)
        {
            Mantissa = mantissa;
            Exponent = exponent;
            Normalize();
        }

        // Нормализация мантиссы, чтобы поддерживать её в диапазоне [1, 10)
        private void Normalize()
        {
            while (Mathf.Abs(Mantissa) >= 10f)
            {
                Mantissa /= 10f;
                Exponent++;
            }

            while (Mathf.Abs(Mantissa) > 0 && Mathf.Abs(Mantissa) < 1f)
            {
                Mantissa *= 10f;
                Exponent--;
            }
        }

        // Сложение двух BigNumber
        public static BigNumber Add(BigNumber a, BigNumber b)
        {
            // Приведение экспонент к общему значению
            if (a.Exponent > b.Exponent)
            {
                float factor = Mathf.Pow(10, a.Exponent - b.Exponent);
                b.Mantissa *= factor;
                b.Exponent = a.Exponent;
            }
            else if (a.Exponent < b.Exponent)
            {
                float factor = Mathf.Pow(10, b.Exponent - a.Exponent);
                a.Mantissa *= factor;
                a.Exponent = b.Exponent;
            }

            // Сложение мантисс
            float resultMantissa = a.Mantissa + b.Mantissa;

            BigNumber result = new BigNumber(resultMantissa, a.Exponent);
            result.Normalize();
            return result;
        }

        // Умножение двух BigNumber
        public static BigNumber Multiply(BigNumber a, BigNumber b)
        {
            float resultMantissa = a.Mantissa * b.Mantissa;
            int resultExponent = a.Exponent + b.Exponent;

            BigNumber result = new BigNumber(resultMantissa, resultExponent);
            result.Normalize();
            return result;
        }

        // Деление двух BigNumber
        public static BigNumber Divide(BigNumber a, BigNumber b)
        {
            float resultMantissa = a.Mantissa / b.Mantissa;
            int resultExponent = a.Exponent - b.Exponent;

            BigNumber result = new BigNumber(resultMantissa, resultExponent);
            result.Normalize();
            return result;
        }

        // Преобразование в строку с сокращениями
        public override string ToString()
        {
            if (Exponent < 3)
                return $"{Mantissa * Mathf.Pow(10, Exponent):#.###}";//Mathf.FloorToInt

            int baseExponent = Exponent / 3;
            char suffix = (char)('A' + baseExponent - 1); // A = 1000, B = 1,000,000, и т.д.

            float shortenedMantissa = Mantissa * Mathf.Pow(10, Exponent % 3);

            return $"{shortenedMantissa:#.###}{suffix}";
        }
    }
}
