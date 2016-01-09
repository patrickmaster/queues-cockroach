using System;
using System.Collections.Generic;
using System.Linq;
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

            if (length == 1)
                return new[] { SolveForSingleChannel(mi.First(), lambda.First()) };

            return SolveForMultipleChannels(m, mi, lambda);
        }

        private SystemParameters SolveForSingleChannel(double mi, double lambda)
        {
            var ro = lambda / mi;

            return new SystemParameters
            {
                AverageEntriesCount = ro / (1 - ro),
                AverageQueueLength = ro * ro / (1 - ro),
                QueueTime = ro / (mi - lambda),
                ServiceTime = 1 / (mi - lambda)
            };
        }

        private IEnumerable<SystemParameters> SolveForMultipleChannels(int[] m, double[] mi, double[] lambda)
        {
            var length = m.Length;

            for (int i = 0; i < length; i++)
                yield return SolveSystem(m[i], mi[i], lambda[i]);
        }

        private SystemParameters SolveSystem(int m, double mi, double lambda)
        {
            var ro = lambda/mi;
            var p0 = (m - ro)/Enumerable.Range(0, m).Sum(s => (m - s)*Math.Pow(ro, s)/Factorial(s));
            var v = Math.Pow(ro, m + 1)*p0/(Factorial(m - 1)*Math.Pow(m - ro, 2));
            var n = v + ro;

            return new SystemParameters
            {
                AverageEntriesCount = n,
                AverageQueueLength = v,
                QueueTime = v/lambda,
                ServiceTime = n/lambda
            };
        }

        private double Factorial(int number)
        {
            var result = number;
            for (int i = 0; i < number; i++)
                result *= i;
            return result;
        }
    }
}