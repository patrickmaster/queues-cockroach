using System;

namespace Queue.ConsoleUI.Solver
{
    internal interface ISolverFactory
    {
        ISolver GetSolver(AlgorithmType problemType);
    }

    class SolverFactory : ISolverFactory
    {
        public ISolver GetSolver(AlgorithmType problemType)
        {
            switch (problemType)
            {
                case AlgorithmType.Jackson:
                    return new JacksonSolver();
                case AlgorithmType.BcmpOne:
                    return new BcmpOneSolver();
                case AlgorithmType.BcmpThree:
                    return new BcmpThreeSolver();
                default:
                    throw new ArgumentOutOfRangeException("problemType", problemType, null);
            }
        }
    }
}