using System;

namespace Queue.Algorithm
{
    internal class SolverFactory : IJacksonSolverFactory, IBcmpOneSolverFactory, IBcmpThreeSolverFactory
    {
        IJacksonSolver IJacksonSolverFactory.GetSolver()
        {
            return new JacksonSolver();
        }

        IBcmpOneSolver IBcmpOneSolverFactory.GetSolver()
        {
            return new BcmpOneSolver();
        }

        IBcmpThreeSolver IBcmpThreeSolverFactory.GetSolver()
        {
            return new BcmpThreeSolver();
        }
    }
}