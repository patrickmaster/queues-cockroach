using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Queue.Algorithm;

namespace Queue.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSolveMethodFirstExample()
        {
            int n = 4;
            double[][] p = new double[n][];
            for (int i = 0; i < n; i++)
            {
                p[i] = new double[n];
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    p[i][j] = i * n + j + 1;
                }
            }
            MatrixSolverWithTwoOnes matrixSolver = new MatrixSolverWithTwoOnes();
            double[] result = matrixSolver.Solve(p);

            Assert.AreEqual(n, result.Length);
            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(1, result[result.Length - 1]);
        }

        [TestMethod]
        public void TestSolveMethodSecondExample()
        {
            int n = 2;
            double[][] p = new double[n][];
            for (int i = 0; i < n; i++)
            {
                p[i] = new double[n];
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    p[i][j] = i * n + j + 1;
                }
            }
            MatrixSolverWithTwoOnes matrixSolver = new MatrixSolverWithTwoOnes();
            double[] result = matrixSolver.Solve(p);

            Assert.AreEqual(n, result.Length);
            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(1, result[result.Length - 1]);
        }

        [TestMethod]
        public void TestSolveMethodThirdExample()
        {
            int n = 3;
            double[][] p = new double[n][];
            for (int i = 0; i < n; i++)
            {
                p[i] = new double[n];
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    p[i][j] = i * n + j + 1;
                }
            }

            MatrixSolverWithTwoOnes matrixSolver = new MatrixSolverWithTwoOnes();
            double[] result = matrixSolver.Solve(p);

            Assert.AreEqual(n, result.Length);
            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(-1.75, result[1]);
            Assert.AreEqual(1, result[result.Length - 1]);
        }

        [TestMethod]
        public void TestSolveMethodFourthExample()
        {
            int n = 4;
            double[][] p = new double[n][];
            for (int i = 0; i < n; i++)
            {
                p[i] = new double[n];
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    p[i][j] = i * n + j + 1;
                }
            }

            MatrixSolverWithTwoOnes matrixSolver = new MatrixSolverWithTwoOnes();
            double[] result = matrixSolver.Solve(p);

            Assert.AreEqual(n, result.Length);
            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(-3, result[2]);
            Assert.AreEqual(1, result[result.Length - 1]);
        }
    }
}
