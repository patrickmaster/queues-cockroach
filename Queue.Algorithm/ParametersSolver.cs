using System.Collections.Generic;
using Queue.Algorithm.Data;

namespace Queue.Algorithm
{
    internal interface IParametersSolver
    {
        IEnumerable<SystemParameters> SolveParameters(int[] m, double[] mi, double[] lambda);
    }

    class ParametersSolver : IParametersSolver
    {
        public IEnumerable<SystemParameters> SolveParameters(int[] m, double[] mi, double[] lambda)
        {
            if (m.Length != mi.Length || m.Length != lambda.Length)
                throw new AlgorithmException("Dimensions do not match");

            var length = m.Length;

            for (int i = 0; i < length; i++)
                yield return SolveParameters(m[i], mi[i], lambda[i]);
        }

        private SystemParameters SolveParameters(int m, double mi, double lambda)
        {
            throw new System.NotImplementedException();
        }
    }
}