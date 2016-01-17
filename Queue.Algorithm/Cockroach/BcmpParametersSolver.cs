using System;
using System.Collections.Generic;
using System.Linq;
using Queue.Algorithm.Data;

namespace Queue.Algorithm.Cockroach
{
    internal interface IBcmpParametersSolver
    {
        IEnumerable<SystemParameters> GetParameters(int[] m, double[][] mi, double[][] lambda, BcmpType[] type);
    }

    internal class BcmpParametersSolver : IBcmpParametersSolver
    {
        public IEnumerable<SystemParameters> GetParameters(int[] m, double[][] mi, double[][] lambda, BcmpType[] type)
        {
            if (m == null) throw new ArgumentNullException("m");
            if (mi == null) throw new ArgumentNullException("mi");
            if (lambda == null) throw new ArgumentNullException("lambda");
            if (type == null) throw new ArgumentNullException("type");

            var length = mi.Length;
            if (length != lambda.Length || length != type.Length)
                throw new ArgumentException("Dimensions do not match");

            var k = new double[length][];
            var ro = GetRo(m, mi, lambda);
            for (var r = 0; r < length; r++)
                k[r] = GetSingleK(m, mi[r], lambda[r], type[r], ro, r);

            for (int i = 0; i < m.Length; i++)
            {
                var serviceTime = Enumerable.Range(0, length).Sum(r => k[r][i] / lambda[r][i]);
                yield return new SystemParameters { ServiceTime = serviceTime };
            }
        }

        private double[][] GetRo(int[] m, double[][] mi, double[][] lambda)
        {
            var rows = m.Length;
            var result = new double[rows][];

            for (int r = 0; r < rows; r++)
            {
                var cols = mi[r].Length;
                result[r] = new double[cols];
                for (int i = 0; i < cols; i++)
                    result[r][i] = lambda[r][i] / (m[r] * mi[r][i]);
            }

            return result;
        }

        private double[] GetSingleK(int[] m, double[] mi, double[] lambda, BcmpType type, double[][] ro, int currentRIndex)
        {
            var length = m.Length;
            if (length != mi.Length || length != lambda.Length)
                throw new ArgumentException("GetSingleResult: dimensions do not match");

            if (type == BcmpType.Three)
                return GetSingleKForThree(mi, lambda);
            if (type == BcmpType.Three)
                return GetSingleKForOne(m, ro, currentRIndex);

            throw new ArgumentException(string.Format("Type array contains unknown element: {0}: {1}", type, (int)type));
        }

        private double[] GetSingleKForOne(int[] m, double[][] ro, int currentRIndex)
        {
            var length = m.Length;
            var result = new double[length];
            var currentR = ro[currentRIndex];

            for (int i = 0; i < length; i++)
            {
                var roI = ro.Sum(x => x[i]);
                var roIReverse = 1 / (1 - roI);
                var miPiPowerToMi = Math.Pow(m[i] * roI, m[i]);
                var kiMiSumMiPi = Enumerable
                    .Range(0, m[i])
                    .Sum(ki => Math.Pow(m[i] * roI, ki) / ki.Factorial());
                var secondFactor = miPiPowerToMi / (m[i].Factorial() * (1 - roI));
                var thirdFactorDenominator = kiMiSumMiPi + miPiPowerToMi * roIReverse / m[i].Factorial();

                result[i] = m[i] * currentR[i] + currentR[i] * roIReverse * secondFactor / thirdFactorDenominator;
            }

            return result;
        }

        private double[] GetSingleKForThree(double[] mi, double[] lambda)
        {
            var length = mi.Length;

            var result = new double[length];
            for (int i = 0; i < length; i++)
                result[i] = lambda[i] / mi[i];

            return result;
        }
    }
}