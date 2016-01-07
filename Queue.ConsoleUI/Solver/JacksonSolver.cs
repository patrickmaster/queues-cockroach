using Queue.Algorithm;

namespace Queue.ConsoleUI.Solver
{
    internal class JacksonSolver : ISolver
    {
        private readonly IJacksonSolverFactory _solverFactory;
        private readonly IJacksonDataProvider _dataProvider;

        public JacksonSolver(IJacksonSolverFactory solverFactory, IJacksonDataProvider dataProvider)
        {
            _solverFactory = solverFactory;
            _dataProvider = dataProvider;
        }

        public SolverResult Solve(string filename)
        {
            var solver = _solverFactory.GetSolver();
            var input = _dataProvider.GetInput(filename);
            return new SolverResult { OutputData = solver.Solve(input) };
        }
    }
}