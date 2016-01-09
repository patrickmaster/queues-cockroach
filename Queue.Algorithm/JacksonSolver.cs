using System.Collections.Generic;
using System.Linq;
using Queue.Algorithm.Data;

namespace Queue.Algorithm
{
    public interface IJacksonSolver
    {
        Output Solve(Input input);
    }

    class JacksonSolver : IJacksonSolver
    {
        private const int MaxIterations = 1000;

        private readonly ICockroachFactory _cockroachFactory;
        private readonly IParametersSolver _parametersSolver;

        public JacksonSolver(ICockroachFactory cockroachFactory, IParametersSolver parametersSolver)
        {
            _cockroachFactory = cockroachFactory;
            _parametersSolver = parametersSolver;
        }

        public Output Solve(Input input)
        {
            var lambdas = GetLambdas(input);
            var cockroach = _cockroachFactory.GetCockroach(input.Mi, lambdas);
            IEnumerable<SystemParameters> currentResult = null;

            for (var i = 0; i < MaxIterations; i++)
            {
                var m = cockroach.GetNext();
                currentResult = _parametersSolver.SolveParameters(m, input.Mi, lambdas);
            }

            if (currentResult == null)
                throw new AlgorithmException(
                    "No result from cockroach. Check max iterations count and the cockroach implementation");

            return CreateResult(currentResult.ToArray());
        }

        private static double[] GetLambdas(Input input)
        {
            var equationResult = MatrixEquation.Solve(input.P);
            var result = new double[equationResult.Length - 2];
            for (var i = 1; i < equationResult.Length - 1; i++)
                result[i - 1] = equationResult[i];
            return result;
        }

        private Output CreateResult(SystemParameters[] parameters)
        {
            return new Output
            {
                Time = parameters.Sum(x => x.ServiceTime),
                SystemStats = parameters
            };
        }
    }
}
