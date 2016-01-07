using Queue.Algorithm;

namespace Queue.ConsoleUI.Solver
{
    internal class BcmpSolver : ISolver
    {
        private readonly IBcmpSolverFactory _factory;
        private readonly IBcmpDataProvider _dataProvider;

        public BcmpSolver(IBcmpSolverFactory factory, IBcmpDataProvider dataProvider)
        {
            _factory = factory;
            _dataProvider = dataProvider;
        }

        public SolverResult Solve(string filename)
        {
            var solver = _factory.GetSolver();
            var input = _dataProvider.GetInput(filename);
            return new SolverResult {OutputData = solver.Solve(input)};
        }
    }
}