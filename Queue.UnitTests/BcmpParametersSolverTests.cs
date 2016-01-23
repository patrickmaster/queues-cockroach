using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Queue.Algorithm;
using Queue.Algorithm.Cockroach;
using Queue.Algorithm.Data;

namespace Queue.UnitTests
{
    [TestClass]
    public class BcmpParametersSolverTests
    {
        private BcmpParametersSolverBetter _solver;

        [TestInitialize]
        public void Initialize()
        {
            _solver = new BcmpParametersSolverBetter();
        }

        [TestMethod]
        public void GetParametersTest()
        {
            var m = new[] {1, 1, 1, 1, 1};
            var mi = new[]
            {
                new[] {0.5, 0.5, 0.25, 0.5, 0.5},
                new[] {0.5, 0.5, 0.25, 0.5, 0.2}
            };
            var lambda = new[]
            {
                new[] {0.25833, 0.041667, 0.041667, 0.041667, 0.06875},
                new[] {0.025, 0.00833, 0.00833, 0, 0.04999}
            };
            var type = new[] {BcmpType.One, BcmpType.One, BcmpType.One, BcmpType.Three, BcmpType.One};

            var result = _solver.GetParameters(m, mi, lambda, type);
        }

        [TestMethod]
        public void GetLambdasTest()
        {
            double[][] e = new[]
            {
                new double[] {1, 1, 1},
                new double[] {1, 1, 1},
                new double[] {0, 1, 0},
                new double[] {1, 0, 1},
                new double[] {0, 1, 0},
                new double[] {0, 1, 0},
                new double[] {1, 0, 5},
                new double[] {1, 1, 1}
            };
            double[][] mi = new[]
            {
                new double[] {67, 67, 67},
                new double[] {8, 8, 8},
                new double[] {60, 60, 60},
                new double[] {8.33, 8.33, 8.33},
                new double[] {12, 12, 12},
                new double[] {0.218, 0.218, 0.218},
                new double[] {1, 1, 1},
                new double[] {0.092, 0.137, 0.053 }
            };
            int[] K = new int[3] {250, 144, 20};
            BcmpType[] type = new BcmpType[]
            {
                BcmpType.One, BcmpType.Three, BcmpType.One, BcmpType.One,
                BcmpType.One, BcmpType.One, BcmpType.One, BcmpType.Three
            };
            int[] m = new int[] {1, 0, 1, 4, 2, 5, 5, 0};
            var lambdas = _solver.FindLambdas(m, mi, e, type, K);
            for (int i = 0; i < lambdas.Length; i++)
            {
                for (int j = 0; j < lambdas[i].Length; j++)
                {
                    Debug.Write(lambdas[i][j] + "\t");
                }
                Debug.WriteLine("");
            }
        }

        [TestMethod]
        public void ClosedParametersTest()
        {
            double[][] mi = new[]
            {
                new double[] {67, 67, 67},
                new double[] {8, 8, 8},
                new double[] {60, 60, 60},
                new double[] {8.33, 8.33, 8.33},
                new double[] {12, 12, 12},
                new double[] {0.218, 0.218, 0.218},
                new double[] {1, 1, 1},
                new double[] {0.092, 0.137, 0.053 }
            };
            var lambda_r = new[] { 20.4729, 11.8015, 0.8208 };
            var ro_ir = new[]
            {
                new[] {0.3056,   0.1761,  0.0123},
                new[] {2.5591,   1.4752,  0.1026},
                new[] {0,    0.1967,  0},
                new[] {0.6144,   0,   0.0246},
                new[] {0,    0.4917,  0},
                new[] {0,    0.8202,  0},
                new[] {0.6824,   0,  0.1368},
                new[] { 222.5316, 86.1423, 15.4870 }
            };
            int[] m = new int[] { 1, 0, 1, 4, 2, 5, 5, 0 };
            var K = new[] { 250, 144, 20 };
            var lambda_ir = new double[mi.Length][];
            for (int i = 0; i < mi.Length; i++)
            {
                lambda_ir[i] = new double[mi[i].Length];
                for (int r = 0; r < mi[i].Length; r++)
                {
                    lambda_ir[i][r] = ro_ir[i][r]*mi[i][r];
                }
            }
            var type = new[]
            {
                BcmpType.One, BcmpType.Three, BcmpType.One, BcmpType.One, BcmpType.One, BcmpType.One, BcmpType.One, BcmpType.Three
            };
            var result = _solver.GetParametersClosedContinuation(m, mi, type, K, lambda_ir);
        }

        [TestMethod]
        public void TestClosedAll()
        {
            double[][] e = new[]
            {
                new double[] {1, 1, 0, 1, 0, 0, 1, 1},
                new double[] {1, 1, 1, 0, 1, 1, 0, 1},
                new double[] {1, 1, 0, 1, 0, 0, 5, 1}
            };
            var type = new[]
            {
                BcmpType.One, BcmpType.Three, BcmpType.One, BcmpType.One, BcmpType.One, BcmpType.One, BcmpType.One, BcmpType.Three
            };
            int[] m = new int[] { 1, 0, 1, 4, 2, 5, 5, 0 };
            var K = new[] { 250, 144, 20 };
            double[][] mi = new[]
            {
                new double[] {67, 8, 60, 8.33, 12, 0.218, 1, 0.092},
                new double[] {67, 8, 60, 8.33, 12, 0.218, 1, 0.137},
                new double[] {67, 8, 60, 8.33, 12, 0.218, 1, 0.053}
            };
            var result = _solver.GetParametersClosed(m, mi, e, type, K);

        }
    }
}
