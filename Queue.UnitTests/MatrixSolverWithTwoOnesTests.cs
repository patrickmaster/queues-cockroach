using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Queue.Algorithm;

namespace Queue.UnitTests
{
    [TestClass]
    public class MatrixSolverWithTwoOnesTests
    {
        [TestMethod]
        public void TestSolveMethodFirstExample()
        {
            int n = 6;
            double[][] p = new double[n][];
            for (int i = 0; i < n; i++)
            {
                p[i] = new double[n];
            }
            
            p[0][0] = 0;
            p[0][1] = 0.33;
            p[0][2] = 0;
            p[0][3] = 0.67;
            p[0][4] = 0;
            p[0][5] = 0;

            p[1][0] = 0;
            p[1][1] = 0;
            p[1][2] = 0.5;
            p[1][3] = 0.25;
            p[1][4] = 0.25;
            p[1][5] = 0;

            p[2][0] = 0;
            p[2][1] = 0;
            p[2][2] = 0;
            p[2][3] = 0;
            p[2][4] = 0.5;
            p[2][5] = 0.5;

            p[3][0] = 0;
            p[3][1] = 0;
            p[3][2] = 0.2;
            p[3][3] = 0;
            p[3][4] = 0.8;
            p[3][5] = 0;

            p[4][0] = 0;
            p[4][1] = 0;
            p[4][2] = 0;
            p[4][3] = 0;
            p[4][4] = 0;
            p[4][5] = 1;

            p[5][0] = 1;
            p[5][1] = 0;
            p[5][2] = 0;
            p[5][3] = 0;
            p[5][4] = 0;
            p[5][5] = 0;

            MatrixSolver matrixSolver = new MatrixSolver();
            double[] result = matrixSolver.Solve(p);

            System.Console.WriteLine("Results");
            for (int i = 0; i < result.Length; i++ )
            {
                System.Console.WriteLine("{0} ", result[i]);
            }
            Assert.AreEqual(n, result.Length);
            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(0.33, result[1]);
            Assert.AreEqual(1, result[result.Length - 1]);
        }

        [TestMethod]
        public void TestSolveMethodSecondExample()
        {
            int n = 3;
            double[][] p = new double[n][];
            for (int i = 0; i < n; i++)
            {
                p[i] = new double[n];
            }

            p[0][0] = 0;
            p[0][1] = 0.25;
            p[0][2] = 0.75;
 
            p[1][0] = 0;
            p[1][1] = 0.5;
            p[1][2] = 0.5;

            p[2][0] = 1;
            p[2][1] = 0;
            p[2][2] = 0;

            MatrixSolver matrixSolver = new MatrixSolver();
            double[] result = matrixSolver.Solve(p);

            System.Console.WriteLine("Results");
            for (int i = 0; i < result.Length; i++)
            {
                System.Console.WriteLine("{0} ", result[i]);
            }
            Assert.AreEqual(n, result.Length);
            Assert.AreEqual(0.5, result[1]);
            Assert.AreEqual(1, result[result.Length - 1]);
        }

        [TestMethod]
        public void TestSolveMethodThirdExampleClosedBCMP()
        {
            int n = 8;
            double[][] p = new double[n][];
            for (int i = 0; i < n; i++)
            {
                p[i] = new double[n];
            }

            p[0][0] = 0;
            p[0][1] = 1;
            p[0][2] = 0;
            p[0][3] = 0;
            p[0][4] = 0;
            p[0][5] = 0;
            p[0][6] = 0;
            p[0][7] = 0;

            p[1][0] = 0;
            p[1][1] = 0;
            p[1][2] = 0;
            p[1][3] = 1;
            p[1][4] = 0;
            p[1][5] = 0;
            p[1][6] = 0;
            p[1][7] = 0;

            p[2][0] = 0;
            p[2][1] = 0;
            p[2][2] = 0;
            p[2][3] = 0;
            p[2][4] = 0;
            p[2][5] = 0;
            p[2][6] = 0;
            p[2][7] = 0;

            p[3][0] = 0;
            p[3][1] = 0;
            p[3][2] = 0;
            p[3][3] = 0;
            p[3][4] = 0;
            p[3][5] = 0;
            p[3][6] = 1;
            p[3][7] = 0;

            p[4][0] = 0;
            p[4][1] = 0;
            p[4][2] = 0;
            p[4][3] = 0;
            p[4][4] = 0;
            p[4][5] = 0;
            p[4][6] = 0;
            p[4][7] = 0;

            p[5][0] = 0;
            p[5][1] = 0;
            p[5][2] = 0;
            p[5][3] = 0;
            p[5][4] = 0;
            p[5][5] = 0;
            p[5][6] = 0;
            p[5][7] = 0;

            p[6][0] = 0;
            p[6][1] = 0;
            p[6][2] = 0;
            p[6][3] = 0;
            p[6][4] = 0;
            p[6][5] = 0;
            p[6][6] = 0;
            p[6][7] = 1;

            p[7][0] = 1;
            p[7][1] = 0;
            p[7][2] = 0;
            p[7][3] = 0;
            p[7][4] = 0;
            p[7][5] = 0;
            p[7][6] = 0;
            p[7][7] = 0;

            MatrixSolver matrixSolver = new MatrixSolver();
            double[] result = matrixSolver.Solve(p);

            System.Console.WriteLine("Results");
            for (int i = 0; i < result.Length; i++)
            {
                System.Console.WriteLine("{0} ", result[i]);
            }
            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(1, result[1]);
            Assert.AreEqual(0, result[2]);
            Assert.AreEqual(1, result[3]);
            Assert.AreEqual(0, result[4]);
            Assert.AreEqual(0, result[5]);
            Assert.AreEqual(1, result[6]);
            Assert.AreEqual(1, result[7]);
        }

        [TestMethod]
        public void TestSolveMethodFourthExampleClosedBCMP()
        {
            int n = 8;
            double[][] p = new double[n][];
            for (int i = 0; i < n; i++)
            {
                p[i] = new double[n];
            }

            p[0][0] = 0;
            p[0][1] = 1;
            p[0][2] = 0;
            p[0][3] = 0;
            p[0][4] = 0;
            p[0][5] = 0;
            p[0][6] = 0;
            p[0][7] = 0;

            p[1][0] = 0;
            p[1][1] = 0;
            p[1][2] = 1;
            p[1][3] = 0;
            p[1][4] = 0;
            p[1][5] = 0;
            p[1][6] = 0;
            p[1][7] = 0;

            p[2][0] = 0;
            p[2][1] = 0;
            p[2][2] = 0;
            p[2][3] = 0;
            p[2][4] = 1;
            p[2][5] = 0;
            p[2][6] = 0;
            p[2][7] = 0;

            p[3][0] = 0;
            p[3][1] = 0;
            p[3][2] = 0;
            p[3][3] = 0;
            p[3][4] = 0;
            p[3][5] = 0;
            p[3][6] = 0;
            p[3][7] = 0;

            p[4][0] = 0;
            p[4][1] = 0;
            p[4][2] = 0;
            p[4][3] = 0;
            p[4][4] = 0;
            p[4][5] = 1;
            p[4][6] = 0;
            p[4][7] = 0;

            p[5][0] = 0;
            p[5][1] = 0;
            p[5][2] = 0;
            p[5][3] = 0;
            p[5][4] = 0;
            p[5][5] = 0;
            p[5][6] = 0;
            p[5][7] = 1;

            p[6][0] = 0;
            p[6][1] = 0;
            p[6][2] = 0;
            p[6][3] = 0;
            p[6][4] = 0;
            p[6][5] = 0;
            p[6][6] = 0;
            p[6][7] = 0;

            p[7][0] = 1;
            p[7][1] = 0;
            p[7][2] = 0;
            p[7][3] = 0;
            p[7][4] = 0;
            p[7][5] = 0;
            p[7][6] = 0;
            p[7][7] = 0;

            MatrixSolver matrixSolver = new MatrixSolver();
            double[] result = matrixSolver.Solve(p);

            System.Console.WriteLine("Results");
            for (int i = 0; i < result.Length; i++)
            {
                System.Console.WriteLine("{0} ", result[i]);
            }
            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(1, result[1]);
            Assert.AreEqual(1, result[2]);
            Assert.AreEqual(0, result[3]);
            Assert.AreEqual(1, result[4]);
            Assert.AreEqual(1, result[5]);
            Assert.AreEqual(0, result[6]);
            Assert.AreEqual(1, result[7]);
        }

        [TestMethod]
        public void TestSolveMethodFithExampleClosedBCMP()
        {
            int n = 8;
            double[][] p = new double[n][];
            for (int i = 0; i < n; i++)
            {
                p[i] = new double[n];
            }

            p[0][0] = 0;
            p[0][1] = 1;
            p[0][2] = 0;
            p[0][3] = 0;
            p[0][4] = 0;
            p[0][5] = 0;
            p[0][6] = 0;
            p[0][7] = 0;

            p[1][0] = 0;
            p[1][1] = 0;
            p[1][2] = 0;
            p[1][3] = 1;
            p[1][4] = 0;
            p[1][5] = 0;
            p[1][6] = 0;
            p[1][7] = 0;

            p[2][0] = 0;
            p[2][1] = 0;
            p[2][2] = 0;
            p[2][3] = 0;
            p[2][4] = 0;
            p[2][5] = 0;
            p[2][6] = 0;
            p[2][7] = 0;

            p[3][0] = 0;
            p[3][1] = 0;
            p[3][2] = 0;
            p[3][3] = 0;
            p[3][4] = 0;
            p[3][5] = 0;
            p[3][6] = 1;
            p[3][7] = 0;

            p[4][0] = 0;
            p[4][1] = 0;
            p[4][2] = 0;
            p[4][3] = 0;
            p[4][4] = 0;
            p[4][5] = 0;
            p[4][6] = 0;
            p[4][7] = 0;

            p[5][0] = 0;
            p[5][1] = 0;
            p[5][2] = 0;
            p[5][3] = 0;
            p[5][4] = 0;
            p[5][5] = 0;
            p[5][6] = 0;
            p[5][7] = 0;

            p[6][0] = 0;
            p[6][1] = 0;
            p[6][2] = 0;
            p[6][3] = 0;
            p[6][4] = 0;
            p[6][5] = 0;
            p[6][6] = 0.8;
            p[6][7] = 0.2;

            p[7][0] = 1;
            p[7][1] = 0;
            p[7][2] = 0;
            p[7][3] = 0;
            p[7][4] = 0;
            p[7][5] = 0;
            p[7][6] = 0;
            p[7][7] = 0;

            MatrixSolver matrixSolver = new MatrixSolver();
            double[] result = matrixSolver.Solve(p);

            System.Console.WriteLine("Results");
            for (int i = 0; i < result.Length; i++)
            {
                System.Console.WriteLine("{0} ", result[i]);
            }
            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(1, result[7]);
        }

        [TestMethod]
        public void TestSolveMethodSixthExampleClosedBCMP()
        {
            int n = 7;
            double[][] p = new double[n][];
            for (int i = 0; i < n; i++)
            {
                p[i] = new double[n];
            }

            p[0][0] = 0;
            p[0][1] = 0.2;
            p[0][2] = 0.1;
            p[0][3] = 0.1;
            p[0][4] = 0.6;
            p[0][5] = 0;
            p[0][6] = 0;

            p[1][0] = 0;
            p[1][1] = 0;
            p[1][2] = 0.1;
            p[1][3] = 0.1;
            p[1][4] = 0.8;
            p[1][5] = 0;
            p[1][6] = 0;

            p[2][0] = 0;
            p[2][1] = 0.1;
            p[2][2] = 0;
            p[2][3] = 0;
            p[2][4] = 0;
            p[2][5] = 0.9;
            p[2][6] = 0;

            p[3][0] = 0;
            p[3][1] = 0;
            p[3][2] = 0;
            p[3][3] = 0;
            p[3][4] = 0.8;
            p[3][5] = 0;
            p[3][6] = 0.2;

            p[4][0] = 0;
            p[4][1] = 0;
            p[4][2] = 0.1;
            p[4][3] = 0;
            p[4][4] = 0;
            p[4][5] = 0.3;
            p[4][6] = 0.6;

            p[5][0] = 0;
            p[5][1] = 0;
            p[5][2] = 0;
            p[5][3] = 0;
            p[5][4] = 0;
            p[5][5] = 0;
            p[5][6] = 1;

            p[6][0] = 1;
            p[6][1] = 0;
            p[6][2] = 0;
            p[6][3] = 0;
            p[6][4] = 0;
            p[6][5] = 0;
            p[6][6] = 0;

            MatrixSolver matrixSolver = new MatrixSolver();
            double[] result = matrixSolver.Solve(p);

            System.Console.WriteLine("Results");
            for (int i = 0; i < result.Length; i++)
            {
                System.Console.WriteLine("{0} ", result[i]);
            }
            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(1, result[6]);
        }

        [TestMethod]
        public void TestSolveMethodSeventhExampleClosedJackosn()
        {
            int n = 4;
            double[][] p = new double[n][];
            for (int i = 0; i < n; i++)
            {
                p[i] = new double[n];
            }

            p[0][0] = 0;
            p[0][1] = 0.5;
            p[0][2] = 0.25;
            p[0][3] = 0.25;

            p[1][0] = 0.5;
            p[1][1] = 0;
            p[1][2] = 0;
            p[1][3] = 0.5;

            p[2][0] = 0;
            p[2][1] = 0.2;
            p[2][2] = 0;
            p[2][3] = 0.8;

            p[3][0] = 0;
            p[3][1] = 0;
            p[3][2] = 1;
            p[3][3] = 0;

            MatrixSolver matrixSolver = new MatrixSolver();
            double[] result = matrixSolver.SolveClosed(p);

            System.Console.WriteLine("Results");
            for (int i = 0; i < result.Length; i++)
            {
                System.Console.WriteLine("{0} ", result[i]);
            }
            Assert.AreEqual(1, result[0]);
        }

        [TestMethod]
        public void TestSolveMethodEightExampleClosedJackosn()
        {
            int n = 4;
            double[][] p = new double[n][];
            for (int i = 0; i < n; i++)
            {
                p[i] = new double[n];
            }

            p[0][0] = 0;
            p[0][1] = 0.2;
            p[0][2] = 0.8;
            p[0][3] = 0;

            p[1][0] = 0;
            p[1][1] = 0;
            p[1][2] = 0.5;
            p[1][3] = 0.5;

            p[2][0] = 0;
            p[2][1] = 0;
            p[2][2] = 0;
            p[2][3] = 1;

            p[3][0] = 1;
            p[3][1] = 0;
            p[3][2] = 0;
            p[3][3] = 0;

            MatrixSolver matrixSolver = new MatrixSolver();
            double[] result = matrixSolver.Solve(p);

            System.Console.WriteLine("Results");
            for (int i = 0; i < result.Length; i++)
            {
                System.Console.WriteLine("{0} ", result[i]);
            }
            Assert.AreEqual(1, result[0]);
        }
    }
}
