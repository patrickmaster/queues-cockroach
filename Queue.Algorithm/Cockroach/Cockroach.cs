using System;

namespace Queue.Algorithm.Cockroach
{
    internal class Cockroach<TOutput> : ICockroach<TOutput>
    {
        private const int CockroachesCount = 100;

        private readonly IValueSolver<TOutput> _valueSolver;
        private TOutput[] _cockroaches;

        public Cockroach(IValueSolver<TOutput> valueSolver)
        {
            _valueSolver = valueSolver;

            InitializeData();
        }

        public TOutput GetNext()
        {
            throw new NotImplementedException();
        }

        private void InitializeData()
        {
            _cockroaches = new TOutput[CockroachesCount];
        }
    }
}
