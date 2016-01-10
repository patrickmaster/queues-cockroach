using System.Linq;
using CSML;
namespace Queue.Algorithm
{
    internal interface IMatrixSolver
    {
        double[] Solve(double[][] p);
    }

    /// <summary>
    /// This solver assumes that in the x*P = 0 equation the first and the
    /// last of x vector equal to 1
    /// </summary>
    class MatrixSolverWithTwoOnes : IMatrixSolver
    {
        public double[] getFinalResult(Matrix finalResult, int rows)
        {
            double[] solution = new double[rows];
            for (int i = 0; i < rows; i++)
            {
                if (i > 0 && i < rows - 1)
                {
                    solution[i] = finalResult[i, 1].Re;
                }
                else
                {
                    solution[i] = 1;
                }

            }

            return solution;
        }

        public static void fillAMatrix(double[][] A, double[][] matrix, int rows, int columns)
        {
            for (int i = 0; i < rows - 2; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (j > 0 && j < columns - 1)
                    {
                        A[i][j - 1] = matrix[i][j];
                    }
                }
            }
        }

        public static void fillTemporaryMatrix(double[][] matrix, double[][] p, int rows, int columns)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (i == j)
                    {
                        matrix[i][j] = p[i][j] - 1;
                    }
                    else
                    {
                        matrix[i][j] = p[j][i];
                    }
                }
            }
        }

        public static double[] getRightSideOfLinearEquatation(int rows, double[][] p)
        {
            double[] rightSide = new double[rows];

            for (int i = 0; i < rows; i++)
            {
                if ((rows - 1 == i) || (0 == i))
                {
                    rightSide[i] = -p[rows - 1][i] - p[0][i] + 1;
                }
                else
                {
                    rightSide[i] = -p[rows - 1][i] - p[0][i];
                }
            }
            return rightSide;
        }

        public static T[,] To2D<T>(T[][] source)
        {
            try
            {
                int FirstDim = source.Length;
                int SecondDim = source.GroupBy(row => row.Length).Single().Key; // throws InvalidOperationException if source is not rectangular

                var result = new T[FirstDim, SecondDim];
                for (int i = 0; i < FirstDim; ++i)
                    for (int j = 0; j < SecondDim; ++j)
                        result[i, j] = source[i][j];

                return result;
            }
            catch (System.InvalidOperationException)
            {
                throw new System.InvalidOperationException("The given jagged array is not rectangular.");
            }
        }

        public double[] Solve(double[][] p)
        {
            int rows = p.Length;
            int columns = p.Length != 0 ? p[0].Length : 0;
            if (rows != columns)
            {
                throw new System.ArgumentException("Number of rows and columns must be equal");
            }

            if ((rows == 2) && (columns == 2))
            {
                double[] exceptionResult = new double[2] { 1, 1 };
                return exceptionResult;
            }

            int n = rows - 2;
            double[] b = getRightSideOfLinearEquatation(rows, p);
            double[][] matrix = new double[rows][];
            double[][] A = new double[n][];
            for (int i = 0; i < rows; i++)
            {
                matrix[i] = new double[columns];
            }
            for (int i = 0; i < n; i++)
            {
                A[i] = new double[n];
            }

            fillTemporaryMatrix(matrix, p, rows, columns);
            fillAMatrix(A, matrix, rows, columns);

            double[] tempB = new double[n];

            for (int i = 0; i < n; i++)
            {
                tempB[i] = b[i];
            }

            double[,] A2D = To2D(A);
            Matrix rightSide = new Matrix(tempB);
            Matrix m = new Matrix(A2D);
            Matrix inverseM = m.Inverse();
            Matrix final = inverseM * rightSide;

            double[] final_result = getFinalResult(final, rows);

            return final_result;
        }
    }
}