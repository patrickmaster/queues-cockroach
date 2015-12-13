using System;
using Queue.Algorithm;
using Queue.ConsoleUI.DataLoading;

namespace Queue.ConsoleUI.Solver
{
    internal class BcmpOneSolver : ISolver
    {
        private readonly IBcmpOneSolverFactory _factory;
        private readonly IBcmpFileDataLoader _dataLoader;

        public BcmpOneSolver(IBcmpOneSolverFactory factory, IBcmpFileDataLoader dataLoader)
        {
            _factory = factory;
            _dataLoader = dataLoader;
        }

        public SolverResult Solve(string filename)
        {
            var solver = _factory.GetSolver();
            var dataProvider = new BcmpDataProvider(filename, _dataLoader);
            return new SolverResult {OutputData = solver.Solve(dataProvider)};
        }
    }
}