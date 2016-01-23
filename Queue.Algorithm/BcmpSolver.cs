using System;
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
        private CockroachResult<int[]> _bestState;

        public BcmpSolver(ICockroachFactory cockroachFactory, IBcmpParametersSolver parametersSolver, ILambdaSolver lambdaSolver)
        {
            _cockroachFactory = cockroachFactory;
            _parametersSolver = parametersSolver;
            _lambdaSolver = lambdaSolver;
        }

        public Output Solve(BcmpInput input)
        {
            double[][] lambda; //lambdas is e in closed network
            if (input.Lambda[0] != 0) //open network
            {
                lambda = GetLambda(input);
            }
            else
            {
                lambda = GetLambdaClosed(input);
            }
            var cockroach = _cockroachFactory.GetCockroach(input, lambda);

            CockroachResult<int[]> bestState = null;
            for (var i = 0; i < MaxIterations; i++)
            {
                IEnumerable<SystemParameters> result;
                bestState = cockroach.GetNext();
                if (input.Lambda[0] != 0) //open network
                {
                    result = _parametersSolver.GetParameters(bestState.State, input.Mi, lambda, input.Type);
                }
                else
                {
                    result = _parametersSolver.GetParametersClosed(bestState.State, input.Mi, lambda, input.Type, input.K);
                }
                LogResult(bestState, result);
            }


            if (bestState == null)
                throw new SolverException(
                    "Got no result from cockroach. Check max iterations count and the cockroach itself");

            IEnumerable<SystemParameters> currentResult;
            if (input.Lambda[0] != 0) //open network
            {
                currentResult = _parametersSolver.GetParameters(bestState.State, input.Mi, lambda, input.Type);
            }
            else //closed network
            {
                currentResult = _parametersSolver.GetParametersClosed(bestState.State, input.Mi, lambda, input.Type, input.K);
            }


            if (currentResult == null)
                throw new AlgorithmException("No result from parameters solver");

            return CreateResult(currentResult, bestState);
        }

        private void LogResult(CockroachResult<int[]> bestState, IEnumerable<SystemParameters> result)
        {
            if (_bestState != bestState)
            {
                _bestState = bestState;
                Console.WriteLine("Value: {0}, Total time: {1}", bestState.Value, result.Sum(x => x.ServiceTime));
            }
        }

        private double[][] GetLambdaClosed(BcmpInput input)
        {
            var length = input.P.Length;
            var result = new double[length][];

            for (int i = 0; i < length; i++)
                result[i] = _lambdaSolver.SolveClosed(input.P[i]);

            return result;
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
