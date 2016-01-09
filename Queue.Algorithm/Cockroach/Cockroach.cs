using System;

namespace Queue.Algorithm.Cockroach
{
    internal class Cockroach<TOutput> : ICockroach<TOutput>
    {
        private const int CockroachesCount = 100;

        private readonly IValueSolver<TOutput> _valueSolver;
        private readonly IRandomizer<TOutput> _randomizer;
        private TOutput[] _cockroaches;

        public Cockroach(IValueSolver<TOutput> valueSolver, IRandomizer<TOutput> randomizer)
        {
            _valueSolver = valueSolver;
            _randomizer = randomizer;

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
