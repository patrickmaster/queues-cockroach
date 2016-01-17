using System.Collections.Generic;
using System.Linq;
using Queue.Algorithm.Cockroach;
using Queue.Algorithm.Data;

namespace Queue.Algorithm
{
    public interface IBcmpSolver
    {
        Output Solve(BcmpInput input);
    }

    internal class BcmpSolver : IBcmpSolver
    {
        private const int MaxIterations = 1000;

        private readonly ILambdaSolver _lambdaSolver;
        private readonly ICockroachFactory _cockroachFactory;
        private readonly IBcmpParametersSolver _parametersSolver;

        public BcmpSolver(ICockroachFactory cockroachFactory, IBcmpParametersSolver parametersSolver, ILambdaSolver lambdaSolver)
        {
            _cockroachFactory = cockroachFactory;
            _parametersSolver = parametersSolver;
            _lambdaSolver = lambdaSolver;
        }

        public Output Solve(BcmpInput input)
        {
            var lambda = GetLambda(input);
            var cockroach = _cockroachFactory.GetCockroach(input, lambda);

            CockroachResult<int[]> bestState = null;
            for (var i = 0; i < MaxIterations; i++)
                bestState = cockroach.GetNext();

            if (bestState == null)
                throw new SolverException(
                    "Got no result from cockroach. Check max iterations count and the cockroach itself");

            var currentResult = _parametersSolver.GetParameters(bestState.State, input.Mi, lambda, input.Type);

            if (currentResult == null)
                throw new AlgorithmException("No result from parameters solver");

            return CreateResult(currentResult, bestState);
        }

        private double[][] GetLambda(BcmpInput input)
        {
            var length = input.P.Length;
            var result = new double[length][];

            for (int i = 0; i < length; i++)
                result[i] = _lambdaSolver.Solve(input.P[i]);

            return result;
        }

        private Output CreateResult(IEnumerable<SystemParameters> parameters, CockroachResult<int[]> cockroachResult)
        {
            var parametersArray = parameters as SystemParameters[] ?? parameters.ToArray();
            return new Output
            {
                Time = parametersArray.Sum(x => x.ServiceTime),
                SystemStats = parametersArray,
                Channels = cockroachResult.State,
                Value = cockroachResult.Value
            };
        }
    }
}
