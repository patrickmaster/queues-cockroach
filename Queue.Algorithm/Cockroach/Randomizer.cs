using System;

namespace Queue.Algorithm.Cockroach
{
    internal static class Randomizer
    {
        private static readonly Random Random = new Random();

        public static int GetRandom(int min, int max)
        {
            return Random.Next(min, max);
        }

        public static int GetRandomPercent()
        {
            return GetRandom(0, 100);
        }
    }
}