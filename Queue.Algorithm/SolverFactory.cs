using System;
using Queue.Common.DI;

namespace Queue.Algorithm
{
    internal class SolverFactory : IJacksonSolverFactory, IBcmpOneSolverFactory, IBcmpThreeSolverFactory
    {
        private readonly IResolver _resolver;

        public SolverFactory(IResolver resolver)
        {
            _resolver = resolver;
        }

        IJacksonSolver IJacksonSolverFactory.GetSolver()
        {
            return _resolver.Create<JacksonSolver>();
        }

        IBcmpOneSolver IBcmpOneSolverFactory.GetSolver()
        {
            return _resolver.Create<BcmpOneSolver>();
        }

        IBcmpThreeSolver IBcmpThreeSolverFactory.GetSolver()
        {
            return _resolver.Create<BcmpThreeSolver>();
        }
    }
}