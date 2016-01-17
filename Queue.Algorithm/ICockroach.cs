namespace Queue.Algorithm
{
    internal interface ICockroach<T>
    {
        CockroachResult<T> GetNext();
    }

    public class CockroachResult<T>
    {
        public CockroachResult(T state, double value)
        {
            State = state;
            Value = value;
        }

        public T State { get; private set; }
        public double Value { get; private set; }
    }
}