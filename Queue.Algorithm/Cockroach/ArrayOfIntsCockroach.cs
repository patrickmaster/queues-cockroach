using System;
using System.Linq;

namespace Queue.Algorithm.Cockroach
{
    public abstract class ArrayOfIntsCockroach : Cockroach<int[]>
    {
        private readonly int _length;
        private readonly int _maxCount;

        protected ArrayOfIntsCockroach(int length, int maxCount)
        {
            _length = length;
            _maxCount = maxCount;
        }

        protected sealed override void Follow(int[] cockroach, int[] leader)
        {
            if (cockroach.Length != leader.Length)
                throw new ArgumentException("cockroach and leader arrays lengths do not match");

            if (cockroach.SequenceEqual(leader))
                return;

            var length = cockroach.Length;
            int index;

            do index = GetRandom(length); while (cockroach[index] == leader[index]);

            cockroach[index] += Math.Sign(leader[index] - cockroach[index]);
        }

        protected override int[] GetRandomState()
        {
            var randomValue = new int[_length];

            for (int i = 0; i < _length; i++)
                randomValue[i] = Randomizer.GetRandom(1, _maxCount);

            return randomValue;
        }

        private int GetRandom(int max)
        {
            return Randomizer.GetRandom(0, max);
        }
    }
}