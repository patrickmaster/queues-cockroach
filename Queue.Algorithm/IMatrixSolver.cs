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
        public double[] getResult(double[][] matrixToEquation, double[] b, int rows, int columns)
        {
            double[][] A = new double[rows - 2][];
            double[] B = new double[rows - 2];

            for (int i = 0; i < rows - 2; i++)
            {
                B[i] = b[i];
            }

            for (int i = 0; i < rows - 2; i++)
            {
                A[i] = new double[columns - 2];
            }

            for (int i = 0; i < rows - 2; i++)
            {
                for (int j = 0; j < columns - 2; j++)
                {
                    A[i][j] = matrixToEquation[i][j];
                }
            }

            double[,] A2D = To2D(A);
            Matrix rightSide = new Matrix(B);
            Matrix m = new Matrix(A2D);
            Matrix inverseM = m.Inverse();
            Matrix final = inverseM * rightSide;
            double[] final_result = getFinalResult(final, rows);
            return final_result;
        }

        public double[] getFinalResult(Matrix finalResult, int rows)
        {
            double[] solution = new double[rows];
            for (int i = 0; i < rows; i++)
            {
                if ((0 == i) || (rows - 1) == i)
                {
                    solution[i] = 1;
                }
                else
                {
                    solution[i] = finalResult[i, 1].Re;
                }

            }
            return solution;
        }

        public static void fillMatrixToEquation(double[][] matrixToEquation, double[][] matrix, int rows, int columns)
        {
            for (int i = 0; i < rows; i++)
            {
                if (0 == i)
                {
                    continue;
                }

                for (int j = 0; j < columns; j++)
                {
                    if ((0 == j) || (columns - 1) == j)
                    {
                        continue;
                    }
                    matrixToEquation[i - 1][j - 1] = matrix[i][j];
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
            double[] b = new double[rows - 1];
            for (int i = 1; i < rows; i++)
            {
                b[i - 1] = -p[0][i];
                if ((rows - 1) == i)
                {
                    b[i - 1] = 1 - p[0][i];
                }
            }
            double[][] matrix = new double[rows][];
            double[][] matrixToEquatation = new double[rows - 1][];
            for (int i = 0; i < rows; i++)
            {
                matrix[i] = new double[columns];
            }
            for (int i = 0; i < rows - 1; i++)
            {
                matrixToEquatation[i] = new double[columns - 2];
            }
            fillTemporaryMatrix(matrix, p, rows, columns);
            fillMatrixToEquation(matrixToEquatation, matrix, rows, columns);

            double[] result = getResult(matrixToEquatation, b, rows, columns);
            return result;
        }
    }
}