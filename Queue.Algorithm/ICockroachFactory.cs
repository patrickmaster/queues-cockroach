using Queue.Algorithm.Cockroach;

namespace Queue.Algorithm
{
    internal interface ICockroachFactory
    {
        ICockroach<int[]> GetCockroach(double[] mi, double[] lambda);
    }

    class CockroachFactory : ICockroachFactory
    {
        private readonly IParametersSolver _parametersSolver;

        public CockroachFactory(IParametersSolver parametersSolver)
        {
            _parametersSolver = parametersSolver;
        }

        public ICockroach<int[]> GetCockroach(double[] mi, double[] lambda)
        {
            var cockroach = new QueueCockroach(_parametersSolver, mi, lambda);
            return cockroach;
        }
    }
}