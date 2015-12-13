namespace Queue.ConsoleUI.Solver
{
    internal interface ISolver
    {
        SolverResult Solve(string filename);
    }
}