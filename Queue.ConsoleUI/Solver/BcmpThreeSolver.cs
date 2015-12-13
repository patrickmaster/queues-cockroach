﻿using System;
using Queue.Algorithm;
using Queue.ConsoleUI.DataLoading;

namespace Queue.ConsoleUI.Solver
{
    internal class BcmpThreeSolver : ISolver
    {
        private readonly IBcmpThreeSolverFactory _factory;
        private readonly IBcmpFileDataLoader _dataLoader;

        public BcmpThreeSolver(IBcmpThreeSolverFactory factory, IBcmpFileDataLoader dataLoader)
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