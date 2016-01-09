using Queue.Algorithm.Cockroach;

namespace Queue.Algorithm
{
    internal class QueueCockroach : Cockroach<int[]>
    {
        public QueueCockroach(IValueSolver<int[]> valueSolver, IRandomizer<int[]> randomizer) 
            : base(valueSolver, randomizer)
        {
        }
    }
}