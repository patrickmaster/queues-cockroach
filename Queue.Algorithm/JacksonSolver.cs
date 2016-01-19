using System;
using System.Collections.Generic;
using System.Linq;
using Queue.Algorithm.Cockroach;
using Queue.Algorithm.Data;

namespace Queue.Algorithm
{
    public interface IJacksonSolver
    {
        Output Solve(JacksonInput input);
    }

    class JacksonSolver : IJacksonSolver
    {
        private const int MaxIterations = 1000;

        private readonly ICockroachFactory _cockroachFactory;
        private readonly IJacksonParametersSolver _jacksonParametersSolver;
        private readonly ILambdaSolver _lambdaSolver;

        private CockroachResult<int[]> _bestState;

        public JacksonSolver(ICockroachFactory cockroachFactory, IJacksonParametersSolver jacksonParametersSolver,
            ILambdaSolver lambdaSolver)
        {
            _cockroachFactory = cockroachFactory;
            _jacksonParametersSolver = jacksonParametersSolver;
            _lambdaSolver = lambdaSolver;
        }

        public Output Solve(JacksonInput input)
        {
            var lambdas = _lambdaSolver.Solve(input.P);
            var cockroach = _cockroachFactory.GetCockroach(input.Mi, lambdas);

            PrintLambdas(lambdas);

            CockroachResult<int[]> bestState = null;
            for (var i = 0; i < MaxIterations; i++)
            {
                bestState = cockroach.GetNext();
                var result = _jacksonParametersSolver.SolveParameters(bestState.State, input.Mi, lambdas);
                LogResult(bestState,result);
            }

            if (bestState == null)
                throw new SolverException(
                    "Got no result from cockroach. Check max iterations count and the cockroach itself");

            var currentResult = _jacksonParametersSolver.SolveParameters(bestState.State, input.Mi, lambdas);

            if (currentResult == null)
                throw new AlgorithmException("No result from parameters solver");

            return CreateResult(currentResult.ToArray(), bestState);
        }

        private void PrintLambdas(double[] lambdas)
        {
            for (int i = 0; i < lambdas.Length; i++)
            {
                Console.WriteLine("Lambda {0}: {1}", i + 1, lambdas[i]);
            }
        }

        private Output CreateResult(SystemParameters[] parameters, CockroachResult<int[]> bestState)
        {
            return new Output
            {
                Time = parameters.Sum(x => x.ServiceTime),
                SystemStats = parameters,
                Channels = bestState.State,
                Value = bestState.Value
            };
        }

        private void LogResult(CockroachResult<int[]> bestState, IEnumerable<SystemParameters> result)
        {
            if (_bestState != bestState)
            {
                _bestState = bestState;
                Console.WriteLine("Value: {0}, Total time: {1}", bestState.Value, result.Sum(x => x.ServiceTime));
            }
        }
    }
}
