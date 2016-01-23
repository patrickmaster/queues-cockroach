using System;
using System.Collections.Generic;
using System.Linq;
using Queue.Algorithm.Data;

namespace Queue.Algorithm
{
    internal interface IJacksonParametersSolver
    {
        IEnumerable<SystemParameters> SolveParameters(int[] m, double[] mi, double[] lambda);
        IEnumerable<SystemParameters> SolveParametersClosed(int[] m, double[] mi, double[] e, int K);
    }

    public class JacksonParametersSolver : IJacksonParametersSolver
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

        public IEnumerable<SystemParameters> SolveParametersClosed(int[] m, double[] mi, double[] e, int K)
        {
            if (m.Length != mi.Length || m.Length != e.Length)
                throw new AlgorithmException("Dimensions do not match");

            var length = m.Length;
            var result = new SystemParameters[length];
            double[] lambda_i = FindLambdas(m, mi, e, K);

            var ro_i = new double[e.Length];
            for (int i = 0; i < e.Length; i++)
            {
                ro_i[i] = lambda_i[i] / mi[i];
            }
            double[] Pmi = FindPmi(m, mi, e, K, ro_i);
            double[] K_i = new double[e.Length];
            for (int i = 0; i < e.Length; i++)
            {
                if (m[i] == 1)
                {
                    K_i[i] = ro_i[i]/(1 - (K - 1.0)/K*ro_i[i]);
                }
                else
                {
                    K_i[i] = m[i]*ro_i[i] + ro_i[i]/(1-(K - m[i] - 1.0)/(K - m[i]) * ro_i[i])*Pmi[i];
                }
            }

            for (int i = 0; i < length; i++)
                result[i] = SolveSystemClosed(m[i], mi[i], K_i[i], lambda_i[i]);

            return result;
        }

        private SystemParameters SolveSystemClosed(int m, double mi, double K_i, double lambda_i)
        {
            double T_i;
            double W_i;
            double Q_i;
            if (lambda_i != 0)
            {
                T_i = K_i / lambda_i;
                W_i = T_i - 1.0 / mi;
                Q_i = lambda_i * W_i;
            }
            else
            {
                T_i = 0;
                W_i = 0;
                Q_i = 0;
            }

            return new SystemParameters
            {
                AverageEntriesCount = K_i,
                AverageQueueLength = Q_i,
                QueueTime = W_i,
                ServiceTime = T_i
            };
        }

        public double[] FindPmi(int[] m, double[] mi, double[] e, int K, double[] ro_i)
        {
            double[] Pmi = new double[e.Length];
            for (int i = 0; i < e.Length; i++)
            {
                double sum = 0;
                for (int j = 0; j < m[i] - 1; j++)
                {
                    sum = sum + Math.Pow(m[i] * ro_i[i], j) / (j.Factorial());
                }
                Pmi[i] = Math.Pow(m[i] * ro_i[i], m[i]) / (m[i].Factorial() * (1 - ro_i[i])) * 1 / (sum + Math.Pow(m[i] * ro_i[i], m[i]) / (m[i].Factorial()) * 1 / (1 - ro_i[i]));
            }
            return Pmi;
        }

        public double[] FindLambdas(int[] m, double[] mi, double[] e, int K)
        {
            var lambda_i = new double[e.Length];
            double lambda_r = 0.00001;
            for (int i = 0; i < e.Length; i++)
            {
                lambda_i[i] = 0.00001;
            }
            double epsilon = 0.00001;
            var ro_i = new double[e.Length];
            var fix_i = new double[e.Length];
            double error;
            bool stop = false;
            while (!stop)
            {
                for (int i = 0; i < e.Length; i++)
                {
                    ro_i[i] = lambda_i[i] / mi[i];
                }
                double[] Pmi = FindPmi(m, mi, e, K, ro_i);
                for (int i = 0; i < m.Length; i++)
                {
                    if (m[i] == 1)
                    {
                        fix_i[i] = (e[i] / mi[i]) / (1.0 - (K - 1.0) / K * ro_i[i]);
                    }
                    else
                    {
                        fix_i[i] = e[i] / mi[i] + (e[i] / (m[i] * mi[i])) / (1.0 - (K - m[i] - 1.0) / (K - m[i]) * ro_i[i]) * Pmi[i];
                    }
                }
                double sumFix = 0;
                var lambda_iOld = lambda_r;
                for (int i = 0; i < lambda_i.Length; i++)
                {
                    sumFix = sumFix + fix_i[i];
                }
                lambda_r = K/sumFix;
                double sumLambdas = 0;
                for (int i = 0; i < lambda_i.Length; i++)
                {
                    sumLambdas = sumLambdas + Math.Pow(lambda_r - lambda_iOld, 2);
                }
                error = Math.Sqrt(sumLambdas);
                if (error <= epsilon)
                {
                    stop = true;
                }
            }
            for (int i = 0; i < e.Length; i++)
            {
                lambda_i[i] = e[i]*lambda_r;
            }
            return lambda_i;
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
            if (lambda > m * mi)
                throw new AlgorithmException(string.Format("lambda {0} is greater than m times mi {1} * {2}", lambda, m, mi));

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