using System.Collections.Generic;
using System.Linq;
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

            int[] m = null;
            for (var i = 0; i < MaxIterations; i++)
                m = cockroach.GetNext();

            var currentResult = _jacksonParametersSolver.SolveParameters(m, input.Mi, lambdas);

            if (currentResult == null)
                throw new AlgorithmException(
                    "No result from cockroach. Check max iterations count and the cockroach implementation");

            return CreateResult(currentResult.ToArray());
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
