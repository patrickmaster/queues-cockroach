using Queue.Algorithm.Cockroach;

namespace Queue.Algorithm
{
    internal class QueueCockroach : Cockroach<int[]>
    {
        public QueueCockroach(IValueSolver<int[]> valueSolver) 
            : base(valueSolver)
        {
        }
    }
}