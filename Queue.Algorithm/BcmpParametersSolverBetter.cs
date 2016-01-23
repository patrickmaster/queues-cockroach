using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Queue.Algorithm.Data;

namespace Queue.Algorithm
{

    internal interface IBcmpParametersSolver
    {
        IEnumerable<SystemParameters> GetParameters(int[] m, double[][] mi, double[][] lambda, BcmpType[] type);
        IEnumerable<SystemParameters> GetParametersClosed(int[] state, double[][] mi, double[][] lambda, BcmpType[] type, int[] K);
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class BcmpParametersSolverBetter : IBcmpParametersSolver
    {
        private double _ro_i;
        private double[][] _k_ri;
        private double[][] _lambda;

        public IEnumerable<SystemParameters> GetParameters(int[] m, double[][] mi, double[][] lambda, BcmpType[] type)
        {
            if (m == null) throw new ArgumentNullException("m");
            if (mi == null) throw new ArgumentNullException("mi");
            if (lambda == null) throw new ArgumentNullException("lambda");
            if (type == null) throw new ArgumentNullException("type");

            _lambda = lambda;
            var rLength = mi.Length;
            if (rLength != _lambda.Length)
                throw new ArgumentException("Dimensions do not match");

            var length = m.Length;

            _k_ri = new double[rLength][];

            for (var r = 0; r < rLength; r++)
            {
                var concreteMi = mi[r];
                var concreteLambda = _lambda[r];

                if (length != concreteMi.Length || length != concreteLambda.Length || length != type.Length)
                    throw new ArgumentException("Dimensions do not match");

                _k_ri[r] = new double[length];

                for (var i = 0; i < length; i++)
                {
                    _ro_i = Enumerable.Range(0, rLength)
                        .Sum(rIndex => _lambda[rIndex][i] / (m[i] * mi[rIndex][i]));
                    _k_ri[r][i] = GetKRi(type[i], m[i], concreteLambda[i], concreteMi[i]);
                }
            }

            var result = new SystemParameters[length];

            for (int i = 0; i < length; i++)
            {
                var serviceTime = Enumerable.Range(0, rLength)
                    .Sum(r => GetServiceTimeElement(r, i));
                result[i] = new SystemParameters { ServiceTime = serviceTime };
            }

            return result;
        }

        public IEnumerable<SystemParameters> GetParametersClosed(int[] state, double[][] mi, double[][] lambda, BcmpType[] type, int[] K)
        {
            throw new NotImplementedException();
        }

        private double GetServiceTimeElement(int r, int i)
        {
            var numerator = _k_ri[r][i];
            var denominator = _lambda[r][i];

            if (Math.Abs(denominator) < 0.0000001)
                if (Math.Abs(numerator) < 0.0000001)
                    return 0;
                else
                    throw new DivideByZeroException("Lambda is zero and K is not");

            return numerator / denominator;
        }

        private double GetKRi(BcmpType type, int m, double lambda, double mi)
        {
            if (type == BcmpType.One)
                return GetKRiForOne(m, lambda, mi);
            if (type == BcmpType.Three)
                return GetKRiForThree(lambda, mi);

            throw new ArgumentException(string.Format("Type array contains unknown element: {0}: {1}", type, (int)type));
        }

        private double GetKRiForOne(int m, double lambda, double mi)
        {
            var ro_ir = lambda / (m * mi);
            var mRo = m * _ro_i;
            var mRoPowerToM = Math.Pow(mRo, m);
            var inverseOneMinusRoi = 1 / (1 - _ro_i);
            var mFactorial = m.Factorial();
            var thirdElementDenominatorSum = Enumerable.Range(0, m)
                .Sum(ki => Math.Pow(mRo, ki) / ki.Factorial());
            var thirdElementDenominator = thirdElementDenominatorSum + mRoPowerToM*inverseOneMinusRoi/mFactorial;

            var result =
                m * ro_ir +
                ro_ir * inverseOneMinusRoi *
                mRoPowerToM * inverseOneMinusRoi / mFactorial /
                thirdElementDenominator;

            return result;
        }

        private double GetKRiForThree(double lambda, double mi)
        {
            return lambda / mi;
        }
    }
}