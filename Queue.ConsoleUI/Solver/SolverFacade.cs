namespace Queue.ConsoleUI.Solver
{
    interface ISolverFacade
    {
        SolverResult Solve(string filename, AlgorithmType problemType);
    }

    internal class SolverFacade : ISolverFacade
    {
        private readonly ISolverFactory _solverFactory;

        public SolverFacade(ISolverFactory solverFactory)
        {
            _solverFactory = solverFactory;
        }

        public SolverResult Solve(string filename, AlgorithmType problemType)
        {
            var solver = _solverFactory.GetSolver(problemType);
            var result = solver.Solve(filename);
            return result;
        }
    }
}
