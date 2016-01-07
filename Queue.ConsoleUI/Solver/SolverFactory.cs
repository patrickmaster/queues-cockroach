using System;
using Queue.Common.DI;
using Queue.ConsoleUI.DI;

namespace Queue.ConsoleUI.Solver
{
    internal interface ISolverFactory
    {
        ISolver GetSolver(AlgorithmType problemType);
    }

    class SolverFactory : ISolverFactory
    {
        private readonly IResolver _resolver;

        public SolverFactory(IResolver resolver)
        {
            _resolver = resolver;
        }

        public ISolver GetSolver(AlgorithmType problemType)
        {
            switch (problemType)
            {
                case AlgorithmType.Jackson:
                    return _resolver.Create<JacksonSolver>();
                case AlgorithmType.Bcmp:
                    return _resolver.Create<BcmpSolver>();
                default:
                    throw new ArgumentOutOfRangeException("problemType", problemType, null);
            }
        }
    }
}