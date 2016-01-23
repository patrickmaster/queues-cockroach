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

        public IEnumerable<SystemParameters> GetParametersClosed(int[] m, double[][] mi, double[][] e, BcmpType[] type, int[] K)
        {
            var transposedE = new double[e[0].Length][];
            var transposedMi = new double[mi[0].Length][];
            for (int i = 0; i < e[0].Length; i++)
            {
                transposedE[i] = new double[e.Length];
                transposedMi[i] = new double[mi.Length];
            }
            for (int i = 0; i < e.Length; i++)
            {
                for (int j = 0; j < e[i].Length; j++)
                {
                    transposedE[j][i] = e[i][j];
                    transposedMi[j][i] = mi[i][j];
                }
            }
            _lambda = FindLambdas(m, transposedMi, transposedE, type, K);
            return GetParametersClosedContinuation(m, transposedMi, type,K,_lambda);
        }
        public double[][] FindLambdas(int[] m, double[][] mi, double[][] e, BcmpType[] type, int[] K)
        {
            double [][] lambda_ir = new double[e.Length][];
            for (int i = 0; i < e.Length; i++)
            {
                lambda_ir[i] = new double[e[0].Length];
            }
            double[] lambda_r = new double[e[0].Length];
            for (int r = 0; r < e[0].Length; r++)
            {
                for (int i = 0; i < e.Length; i++)
                {
                    lambda_ir[i][r] = 0.00001;
                }
                lambda_r[r] = 0.00001;
            }
            for (int r = 0; r < e[0].Length; r++)
            {
                double epsilon = 0.00001;
                var ro_i = new double[e.Length];
                var fix_ir = new double[e.Length][];
                for (int i = 0; i < e.Length; i++)
                {
                    fix_ir[i] = new double[e[i].Length];
                }
                double error;
                bool stop = false;
                while (!stop)
                {
                    for (int i = 0; i < e.Length; i++)
                    {
                        for (int rr = 0; rr < e[0].Length; rr++)
                        {
                            ro_i[i] += (lambda_ir[i][rr] / mi[i][rr]);
                        }
                    }
                    double[] Pmi = FindPmi(m, ro_i);
                    for (int i = 0; i < m.Length; i++)
                    {
                        if (type[i] == BcmpType.One && m[i] == 1)
                        {
                            fix_ir[i][r] = (e[i][r] / mi[i][r]) / (1.0 - (K[r] - 1.0) / K[r] * ro_i[i]);
                        }
                        else if (type[i] == BcmpType.One && m[i] > 1)
                        {
                            fix_ir[i][r] = e[i][r] / mi[i][r] + (e[i][r] / (m[i] * mi[i][r])) / (1.0 - (K[r] - m[i] - 1.0) / (K[r] - m[i]) * ro_i[i]) * Pmi[i];
                        }
                        else
                        {
                            fix_ir[i][r] = e[i][r] / mi[i][r];
                        }
                    }
                    double sumFix = 0;
                    var lambda_iOld = lambda_r[r];
                    for (int i = 0; i < lambda_ir.Length; i++)
                    {
                        sumFix += fix_ir[i][r];
                    }
                    lambda_r[r] = K[r] / sumFix;
                    double sumLambdas = 0;
                    for (int i = 0; i < lambda_ir.Length; i++)
                    {
                        sumLambdas += Math.Pow(lambda_r[r] - lambda_iOld, 2);
                    }
                    error = Math.Sqrt(sumLambdas);
                    if (error <= epsilon)
                    {
                        stop = true;
                    }
                }
                for (int i = 0; i < e.Length; i++)
                {
                    lambda_ir[i][r] = e[i][r] * lambda_r[r];
                }
            }
            return lambda_ir;
        }
        public IEnumerable<SystemParameters> GetParametersClosedContinuation(int[] m, double[][] mi, BcmpType[] type, int[] K, double[][] lambda)
        {
            //first dimension is system, second is class
            double[][] ro_ir = new double[mi.Length][];
            double[] ro_i = new double[m.Length];
            for (int i = 0; i < mi.Length; i++)
            {
                ro_ir[i] = new double[mi[i].Length];
                ro_i[i] = 0;
                for (int r = 0; r < mi[i].Length; r++)
                {
                    ro_ir[i][r] = lambda[i][r]/mi[i][r];
                    ro_i[i] = ro_i[i] + ro_ir[i][r];
                }
            }
            double[] Pmi = new double[ro_i.Length];
            Pmi = FindPmi(m, ro_i);
            double[][] K_ir = new double[mi.Length][];
            double[][] Q_ir = new double[mi.Length][];
            double[][] T_ir = new double[mi.Length][];
            double[][] W_ir = new double[mi.Length][];
            double[] K_i = new double[m.Length];
            double[] Q_i = new double[m.Length];
            double[] T_i = new double[m.Length];
            double[] W_i = new double[m.Length];

            for (int i = 0; i < mi.Length; i++)
            {
                K_ir[i] = new double[mi[i].Length];
                Q_ir[i] = new double[mi[i].Length];
                T_ir[i] = new double[mi[i].Length];
                W_ir[i] = new double[mi[i].Length];
                K_i[i] = 0;
                Q_i[i] = 0;
                T_i[i] = 0;
                W_i[i] = 0;
                for (int r = 0; r < mi[i].Length; r++)
                {
                    if (type[i] == BcmpType.One)
                    {
                        if (m[i] == 1)
                        {
                            K_ir[i][r] = ro_ir[i][r] / (1 - (K[r] - 1.0) / K[r] * ro_i[i]);
                        }
                        else
                        {
                            K_ir[i][r] = m[i] * ro_ir[i][r] + ro_ir[i][r] / (1 - (K[r] - m[i] - 1.0) / (K[r] - m[i]) * ro_i[i]) * Pmi[i];
                        }
                    }
                    else
                    {
                        K_ir[i][r] = ro_ir[i][r];
                    }
                    if (ro_ir[i][r] != 0)
                    {
                        T_ir[i][r] = K_ir[i][r] / lambda[i][r];
                        if (type[i] == BcmpType.One)
                        {
                            W_ir[i][r] = T_ir[i][r] - 1.0 / mi[i][r];
                        }
                        else
                        {
                            W_ir[i][r] = 0;
                        }
                        Q_ir[i][r] = lambda[i][r] * W_ir[i][r];
                        K_i[i] = K_i[i] + K_ir[i][r];
                        Q_i[i] = Q_i[i] + Q_ir[i][r];
                        T_i[i] = T_i[i] + T_ir[i][r];
                        W_i[i] = W_i[i] + W_ir[i][r];
                    }
                    else
                    {
                        T_ir[i][r] = 0;
                        Q_ir[i][r] = 0;
                        W_ir[i][r] = 0;
                    }
                }
            }
            PrintParameters(K_ir, T_ir, Q_ir, W_ir);
            var result = new SystemParameters[mi.Length];
            for (int i = 0; i < mi.Length; i++)
            {
                result[i] = new SystemParameters
                    {
                        AverageEntriesCount = K_i[i],
                        AverageQueueLength = Q_i[i],
                        QueueTime = W_i[i],
                        ServiceTime = T_i[i]
                };
            }

            return result;
        }

        private void PrintParameters(double[][] K_ir, double[][] T_ir, double[][] Q_ir, double[][] W_ir)
        {
            for (int i = 0; i < K_ir.Length; i++)
            {
                Console.WriteLine("System " + i + ": ");
                for (int r = 0; r < K_ir[i].Length; r++)
                {
                    Console.Write("Class " + r + ": ");
                    Console.Write("Average Entries Count = " + K_ir[i][r]);
                    Console.Write(" Average Queue Length = " + Q_ir[i][r]);
                    Console.Write(" Queue Time = " + W_ir[i][r]);
                    Console.WriteLine(" Service Time = " + T_ir[i][r]);
                }
            }
        }

        public double[] FindPmi(int[] m, double[] ro_i)
        {
            double[] Pmi = new double[m.Length];
            for (int i = 0; i < m.Length; i++)
            {
                double sum = 0;
                for (int j = 0; j < m[i] - 1; j++)
                {
                    sum = sum + Math.Pow(m[i] * ro_i[i], j) / (j.Factorial());
                }
                Pmi[i] = Math.Pow(m[i] * ro_i[i], m[i]) / (m[i].Factorial() * (1.0 - ro_i[i])) * 1 / (sum + Math.Pow(m[i] * ro_i[i], m[i]) / (m[i].Factorial()) * 1.0 / (1.0 - ro_i[i]));
            }
            return Pmi;
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