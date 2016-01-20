using Queue.Algorithm.Data;

namespace Queue.Algorithm
{
    internal interface ILambdaSolver
    {
        double[] Solve(double[][] p);
        double[] SolveClosed(double[][] e);
    }
    
    class LambdaSolver : ILambdaSolver
    {
        private readonly IMatrixSolver _matrixSolver;

        public LambdaSolver(IMatrixSolver matrixSolver)
        {
            _matrixSolver = matrixSolver;
        }

        public double[] Solve(double[][] p)
        {
            var result = _matrixSolver.Solve(p);
            return GetLambdas(result);
        }

        public double[] SolveClosed(double[][] e)
        {
            var result = _matrixSolver.SolveClosed(e);
            return GetLambdas(result);
        }

        private double[] GetLambdas(double[] equationResult)
        {
            var result = new double[equationResult.Length - 2];
            for (var i = 1; i < equationResult.Length - 1; i++)
                result[i - 1] = equationResult[i];
            return result;
        }
    }
}