using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Queue.Algorithm.Data;

namespace Queue.ConsoleUI.Solver
{
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
