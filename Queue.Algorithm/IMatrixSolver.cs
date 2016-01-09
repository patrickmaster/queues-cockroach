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
        
        public double[] Solve(double[][] p)
        {
            throw new System.NotImplementedException();
        }
    }
}