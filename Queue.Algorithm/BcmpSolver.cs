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

            CockroachResult<int[]> m = null;
            for (var i = 0; i < MaxIterations; i++)
                m = cockroach.GetNext();

            var currentResult = _parametersSolver.GetParameters(m.State, input.Mi, lambda, input.Type);

            return CreateResult(currentResult);
        }

        private double[][] GetLambda(BcmpInput input)
        {
            var length = input.P.Length;
            var result = new double[length][];

            for (int i = 0; i < length; i++)
                result[i] = _lambdaSolver.Solve(input.P[i]);

            return result;
        }

        private Output CreateResult(IEnumerable<SystemParameters> parameters)
        {
            var parametersArray = parameters as SystemParameters[] ?? parameters.ToArray();
            return new Output
            {
                Time = parametersArray.Sum(x => x.ServiceTime),
                SystemStats = parametersArray
            };
        }
    }
}
