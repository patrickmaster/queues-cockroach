using System.Linq;
using CSML;
namespace Queue.Algorithm
{
    internal interface IMatrixSolver
    {
        double[] Solve(double[][] p); // first column zeros and last element in first column is 1 (examples BCMP.Closed.xml, Jackson.xml, Jackson2.xml, jackson3.xml)
        double[] SolveClosed(double[][] e); //other cases, especially ClosedJackson.xml file
    }

    /// <summary>
    /// This solver assumes that in the x*P = 0 equation the first and the
    /// last of x vector equal to 1
    /// </summary>
    class MatrixSolver : IMatrixSolver
    {
        public static double[,] TrimArray(int rowToRemove, int columnToRemove, double[,] originalArray)
        {
            double[,] result = new double[originalArray.GetLength(0) - 1, originalArray.GetLength(1) - 1];
            for (int i = 0, j = 0; i < originalArray.GetLength(0); i++)
            {
                if (i == rowToRemove)
                    continue;

                for (int k = 0, u = 0; k < originalArray.GetLength(1); k++)
                {
                    if (k == columnToRemove)
                        continue;

                    result[j, u] = originalArray[i, k];
                    u++;
                }
                j++;
            }
            return result;
        }

        public bool isRowWithZeros(double [][] table, int row)
        {
            for (int i = 0; i < table[row].Length; i++)
            {
                if(table[row][i] != 0)
                {
                    return false;
                }
            }
            return true;
        }

        public int[] getArrayOfRemovedRowsAndColumns(double [][]p, int rows, int columns)
        {
            int cnt = 0;
            for (int i = 0; i < rows; i++)
            {
                if (isRowWithZeros(p, i))
                {
                    cnt++;
                }
            }

            int[] result = new int[cnt];
            int n = 0;
            for (int i = 0; i < rows; i++)
            {
                if (isRowWithZeros(p, i))
                {
                    result[n] = i;
                    n++;
                }
            }
            return result;
        }

        public int[] getArrayOfRemovedRowsAndColunsAndRemoveRows(double[][] A, int rows, int columns, double [,] A2D)
        {
            int cnt = 0;
            for (int i = 0; i < rows - 2; i++)
            {
                    if (isRowWithZeros(A, i))
                    {
                        cnt++;
                    }
            }

            int[] result = new int [cnt];
            int n = 0;
            for (int i = 0; i < rows - 2; i++)
            {
                    if (isRowWithZeros(A, i))
                    {
                        result[n] = i;
                        n++;
                    }
            }
            return result;
        }

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
            int[] removedRowsAndColumns = getArrayOfRemovedRowsAndColunsAndRemoveRows(A, rows, columns, A2D);
            if (removedRowsAndColumns.Length > 0)
            {
                for (int i = 0; i < removedRowsAndColumns.Length; i++)
                {
                    A2D = TrimArray(removedRowsAndColumns[i] - i, removedRowsAndColumns[i] - i, A2D);
                }

                double[] bWhenRemoved = new double[A2D.GetLength(0)];
                int cnt = 0;
                for(int i = 0; i < rows - 2; i++)
                {
                    if(isElementInArray(i, removedRowsAndColumns) == false)
                    {
                        bWhenRemoved[cnt] = B[i];
                        cnt++;
                    }
                }
                Matrix rightSide = new Matrix(bWhenRemoved);
                Matrix m = new Matrix(A2D);
                Matrix inverseM = m.Inverse();
                Matrix final = inverseM * rightSide;
                double[] final_result = getFinalResult(final, rows, removedRowsAndColumns);
                return final_result;  
            }
            else 
            {
                Matrix rightSide = new Matrix(B);
                Matrix m = new Matrix(A2D);
                Matrix inverseM = m.Inverse();
                Matrix final = inverseM * rightSide;

                double[] final_result = getFinalResult(final, rows, removedRowsAndColumns);
                return final_result;  
            }
        }

        public double[] getFinalResult(Matrix finalResult, int rows, int [] removedRowsAndColumns)
        {
            if(removedRowsAndColumns.Length == 0)
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
            else
            {
                for(int i = 0; i < removedRowsAndColumns.Length; i++)
                {
                    removedRowsAndColumns[i] = removedRowsAndColumns[i] + 1; // first row comes back
                }
                double[] solution = new double[rows];
                int cnt = 1;
                for (int i = 0; i < rows; i++)
                {
                    if ((0 == i) || (rows - 1) == i)
                    {
                        solution[i] = 1;
                    }
                    else if (isElementInArray(i, removedRowsAndColumns))
                    {
                        solution[i] = 0;
                    }
                    else
                    {
                        solution[i] = finalResult[cnt, 1].Re;
                        cnt++;
                    }

                }
                return solution;
            }
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

        public static bool isElementInArray(int elem, int [] table)
        {
            for(int i = 0; i < table.Length; i++)
            {
                if(table[i] == elem)
                {
                    return true;
                }
            }
            return false;
        }

        public static void fillTemporaryMatrix(double[][] matrix, double[][] p, int rows, int columns, int [] removedRows)
        {
            if (removedRows.Length > 0)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if (i == j && isElementInArray(i, removedRows) == false)
                        {
                            matrix[i][j] = p[i][j] - 1;
                        }
                        else if (i == j && isElementInArray(i, removedRows) == true)
                        {
                            matrix[i][j] = 0;
                        }
                        else
                        {
                            matrix[i][j] = p[j][i];
                        }
                    }
                }
            }
            else
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

        public static void fillTemporaryMatrixForClosedJackson(double[][] matrix,
                                                            double[][] p,
                                                            int rows,
                                                            int columns)
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

        public static void fillMatrixToEquationForClosedJackson(double[][] matrixToEquation,
                                                               double[][] matrix,
                                                               int rows,
                                                               int columns)
        {
            for (int i = 0; i < rows; i++)
            {
                if (0 == i)
                {
                    continue;
                }

                for (int j = 0; j < columns; j++)
                {
                    if (0 == j)
                    {
                        continue;
                    }
                    matrixToEquation[i - 1][j - 1] = matrix[i][j];
                }
            }
        }

        public double[] getFinalResultForClosedJackson(Matrix finalResult,
                                                       int rows)
        {
            double[] solution = new double[rows];
            for (int i = 0; i < rows; i++)
            {
                if (0 == i)
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

            int[] arrayOfRemovedColumns = getArrayOfRemovedRowsAndColumns(p, rows, columns);
            fillTemporaryMatrix(matrix, p, rows, columns, arrayOfRemovedColumns);
            fillMatrixToEquation(matrixToEquatation, matrix, rows, columns);
            double[] result = getResult(matrixToEquatation, b, rows, columns);
            return result;
        }

        public double[] SolveClosed(double[][] e)
        {
            int rows = e.Length;
            int columns = e.Length != 0 ? e[0].Length : 0;
            if (rows != columns)
            {
                throw new System.ArgumentException("Number of rows and columns must be equal");
            }
            double[] b = new double[rows -1];
            for (int i = 0; i < rows; i++)
            {
                if(0 == i)
                {
                    continue;
                }
                else
                {
                    b[i-1] = -e[0][i];
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
                matrixToEquatation[i] = new double[columns - 1];
            }

            fillTemporaryMatrixForClosedJackson(matrix, e, rows, columns);
            fillMatrixToEquationForClosedJackson(matrixToEquatation, matrix, rows, columns);
            double[,] A2D = To2D(matrixToEquatation);
            Matrix rightSide = new Matrix(b);
            Matrix m = new Matrix(A2D);
            Matrix inverseM = m.Inverse();
            Matrix final = inverseM * rightSide;
            double[] final_result = getFinalResultForClosedJackson(final, rows);
            return final_result;  

        }
    }
}