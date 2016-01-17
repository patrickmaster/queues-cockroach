using System;

namespace Queue.Algorithm
{
    internal static class MathExtensions
    {
        public static long Factorial(this int number)
        {
            if (number < 0)
                throw new ArgumentException("Number cannot be less than zero");

            long result = number;
            for (var i = 0; i < number; i++)
                result *= i;
            return result;
        }
    }
}
