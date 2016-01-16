using System;
using Queue.Common.DI;

namespace Queue.Algorithm
{
    internal class SolverFactory : IJacksonSolverFactory, IBcmpSolverFactory
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

        IBcmpSolver IBcmpSolverFactory.GetSolver()
        {
            return _resolver.Create<BcmpSolver>();
        }

    }
}