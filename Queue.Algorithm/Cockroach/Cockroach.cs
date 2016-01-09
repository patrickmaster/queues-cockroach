using System;

namespace Queue.Algorithm.Cockroach
{
    internal abstract class Cockroach<TState> : ICockroach<TState>
    {
        private const int CockroachesCount = 100;

        private TState[] _cockroaches;

        protected Cockroach()
        {
            InitializeData();
        }

        public TState GetNext()
        {
            throw new NotImplementedException();
        }

        protected abstract double GetValue(TState state);

        protected abstract TState GetRandom();

        private void InitializeData()
        {
            _cockroaches = new TState[CockroachesCount];
        }
    }
}
