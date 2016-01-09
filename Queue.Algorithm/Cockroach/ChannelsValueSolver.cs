using System.Linq;

namespace Queue.Algorithm.Cockroach
{
    class ChannelsValueSolver : IValueSolver<int[]>
    {
        private const double WinFactor = 1;
        private const double LossFactor = 1;

        private readonly double[] _mi;
        private readonly double[] _lambda;
        private readonly IParametersSolver _parametersSolver;

        public ChannelsValueSolver(double[] mi, double[] lambda, IParametersSolver parametersSolver)
        {
            _mi = mi;
            _lambda = lambda;
            _parametersSolver = parametersSolver;
        }

        public double GetValue(int[] m)
        {
            var parameters = _parametersSolver.SolveParameters(m, _mi, _lambda);
            return WinFactor * parameters.Sum(x => x.ServiceTime) - LossFactor * m.Sum();
        }

        public int[] GetRandomValue()
        {
            throw new System.NotImplementedException();
        }
    }
}