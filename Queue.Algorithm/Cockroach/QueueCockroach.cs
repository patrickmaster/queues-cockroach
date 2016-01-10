using System;
using System.Linq;

namespace Queue.Algorithm.Cockroach
{
    internal class QueueCockroach : ArrayOfIntsCockroach
    {
        private const double WinFactor = 1;
        private const double LossFactor = 1;
        private const int MaxChannelsCount = 5;

        private readonly IParametersSolver _parametersSolver;
        private readonly double[] _mi;
        private readonly double[] _lambda;
        private readonly int _length;

        public QueueCockroach(IParametersSolver parametersSolver, double[] mi, double[] lambda)
        {
            _parametersSolver = parametersSolver;

            if (mi.Length != lambda.Length)
                throw new ArgumentException("mi and lambda dimensions do not match");

            _mi = mi;
            _lambda = lambda;
            _length = mi.Length;
        }

        protected override double GetValue(int[] state)
        {
            var parameters = _parametersSolver.SolveParameters(state, _mi, _lambda);
            return WinFactor * parameters.Sum(x => x.ServiceTime) - LossFactor * state.Sum();
        }

        protected override int[] GetRandomState()
        {
            var randomValue = new int[_length];

            for (int i = 0; i < _length; i++)
                randomValue[i] = Randomizer.GetRandom(0, MaxChannelsCount);

            return randomValue;
        }
    }
}