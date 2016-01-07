using System;
using Queue.Algorithm;
using Queue.ConsoleUI.DataLoading;

namespace Queue.ConsoleUI.Solver
{
    internal class BcmpSolver : ISolver
    {
        private readonly IBcmpSolverFactory _factory;
        private readonly IBcmpFileDataLoader _dataLoader;

        public BcmpSolver(IBcmpSolverFactory factory, IBcmpFileDataLoader dataLoader)
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