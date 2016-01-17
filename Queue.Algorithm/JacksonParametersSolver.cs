using System;
using System.Collections.Generic;
using System.Linq;
using Queue.Algorithm.Data;

namespace Queue.Algorithm
{
    internal interface IJacksonParametersSolver
    {
        IEnumerable<SystemParameters> SolveParameters(int[] m, double[] mi, double[] lambda);
    }

    internal class JacksonParametersSolver : IJacksonParametersSolver
    {
        public IEnumerable<SystemParameters> SolveParameters(int[] m, double[] mi, double[] lambda)
        {
            if (m.Length != mi.Length || m.Length != lambda.Length)
                throw new AlgorithmException("Dimensions do not match");

            var length = m.Length;
            var result = new SystemParameters[length];

            for (int i = 0; i < length; i++)
                result[i] = SolveSystem(m[i], mi[i], lambda[i]);

            return result;
        }

        private SystemParameters SolveSystem(int m, double mi, double lambda)
        {
            if (m == 1)
                return SolveForSingleChannel(mi, lambda);
            if (m > 1)
                return SolveForMultipleChannels(m, mi, lambda);

            throw new ArgumentException("m is less than one");
        }

        private SystemParameters SolveForSingleChannel(double mi, double lambda)
        {
            if (lambda > mi)
                throw new AlgorithmException(string.Format("lambda {0} is greater than mi {1}", lambda, mi));

            var ro = lambda / mi;

            return new SystemParameters
            {
                AverageEntriesCount = ro / (1 - ro),
                AverageQueueLength = ro * ro / (1 - ro),
                QueueTime = ro / (mi - lambda),
                ServiceTime = 1 / (mi - lambda)
            };
        }

        private SystemParameters SolveForMultipleChannels(int m, double mi, double lambda)
        {
            if (lambda > mi)
                throw new AlgorithmException(string.Format("lambda {0} is greater than mi {1}", lambda, mi));

            var ro = lambda / mi;
            var sumS0MMinus1 = Enumerable
                .Range(0, m)
                .Sum(s => GetSumElement(m, s, ro));
            var p0 = (m - ro) / sumS0MMinus1;
            var v = Math.Pow(ro, m + 1) * p0 / ((m - 1).Factorial() * Math.Pow(m - ro, 2));
            var n = v + ro;

            return new SystemParameters
            {
                AverageEntriesCount = n,
                AverageQueueLength = v,
                QueueTime = v / lambda,
                ServiceTime = n / lambda
            };
        }

        private static double GetSumElement(int m, int s, double ro)
        {
            var numerator = (m - s) * Math.Pow(ro, s);
            var denominator = s.Factorial();
            return numerator / denominator;
        }
    }
}