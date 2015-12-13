using System;
using Queue.Algorithm;
using Queue.ConsoleUI.DataLoading;

namespace Queue.ConsoleUI.Solver
{
    internal class JacksonSolver : ISolver
    {
        private readonly IJacksonSolverFactory _solverFactory;
        private readonly IJacksonFileDataLoader _dataLoader;

        public JacksonSolver(IJacksonSolverFactory solverFactory, IJacksonFileDataLoader dataLoader)
        {
            _solverFactory = solverFactory;
            _dataLoader = dataLoader;
        }

        public SolverResult Solve(string filename)
        {
            var solver = _solverFactory.GetSolver();
            var dataProvider = new JacksonDataProvider(filename, _dataLoader);
            return new SolverResult { OutputData = solver.Solve(dataProvider) };
        }
    }
}