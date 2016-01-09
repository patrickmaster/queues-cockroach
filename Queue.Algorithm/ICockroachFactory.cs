using Queue.Algorithm.Cockroach;
using Queue.Algorithm.Data;

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
            var valueSolver = new ChannelsValueSolver(mi, lambda, _parametersSolver);
            var cockroach = new QueueCockroach(valueSolver);
            return cockroach;
        }
    }
}