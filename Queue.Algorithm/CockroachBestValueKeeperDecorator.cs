namespace Queue.Algorithm
{
    class CockroachBestValueKeeperDecorator<T> : ICockroach<T>
    {
        private readonly ICockroach<T> _cockroach;

        private CockroachResult<T> _bestResult;

        public CockroachBestValueKeeperDecorator(ICockroach<T> cockroach)
        {
            _cockroach = cockroach;
        }

        public CockroachResult<T> GetNext()
        {
            var currentResult = _cockroach.GetNext();

            if (_bestResult == null || _bestResult.Value < currentResult.Value)
                _bestResult = currentResult;

            return _bestResult;
        }
    }
}