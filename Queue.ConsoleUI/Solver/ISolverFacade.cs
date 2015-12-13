namespace Queue.ConsoleUI.Solver
{
    interface ISolverFacade
    {
        SolverResult Solve(string filename, AlgorithmType problemType);
    }
}