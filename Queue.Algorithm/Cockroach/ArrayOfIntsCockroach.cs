using System;
using System.Linq;

namespace Queue.Algorithm.Cockroach
{
    public abstract class ArrayOfIntsCockroach : Cockroach<int[]>
    {
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

        private int GetRandom(int max)
        {
            return Randomizer.GetRandom(0, max);
        }
    }
}