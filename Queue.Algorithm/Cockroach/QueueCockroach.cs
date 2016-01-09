using System.Linq;

namespace Queue.Algorithm.Cockroach
{
    internal class QueueCockroach : Cockroach<int[]>
    {
        private const double WinFactor = 1;
        private const double LossFactor = 1;

        private readonly IParametersSolver _parametersSolver;
        private readonly double[] _mi;
        private readonly double[] _lambda;

        public QueueCockroach(IParametersSolver parametersSolver, double[] mi, double[] lambda)
        {
            _parametersSolver = parametersSolver;
            _mi = mi;
            _lambda = lambda;
        }

        protected override double GetValue(int[] state)
        {
            var parameters = _parametersSolver.SolveParameters(state, _mi, _lambda);
            return WinFactor * parameters.Sum(x => x.ServiceTime) - LossFactor * state.Sum();
        }

        protected override int[] GetRandom()
        {
            throw new System.NotImplementedException();
        }
    }
}