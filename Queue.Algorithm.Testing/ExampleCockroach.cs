using System;
using Queue.Algorithm.Cockroach;

namespace Queue.Algorithm.Testing
{
    class ExampleCockroach : ArrayOfIntsCockroach
    {
        private readonly int _maxCount;
        private const int Length = 5;

        private readonly Random _random = new Random();

        public ExampleCockroach(int maxCount)
        {
            _maxCount = maxCount;
        }

        protected override double GetValue(int[] state)
        {
            return GetFunctionValue(state);
        }

        public static double GetFunctionValue(int[] state)
        {
            var functionValue = Math.Pow(state[0], 5)/20000 +
                                Math.Pow(state[1], 4)/3000 +
                                Math.Pow(state[2], 3)/1000 +
                                Math.Pow(state[3], 2)/5 +
                                state[4] +
                                4;

            return 1000 - Math.Abs(functionValue);
        }

        protected override int[] GetRandomState()
        {
            var randomValue = new int[Length];

            for (int i = 0; i < Length; i++)
                randomValue[i] = GetRandom();

            return randomValue;
        }

        private int GetRandom()
        {
            return _random.Next(0, _maxCount);
        }
    }
}
