using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Queue.Algorithm.Data;
using Queue.ConsoleUI.DataLoading;

namespace Queue.ConsoleUI.Solver
{
    interface ISolver
    {
        SolverResult Solve(string filename);
    }

    class Solver : ISolver
    {
        private readonly IFileDataLoader _dataLoader;

        public Solver(IFileDataLoader dataLoader)
        {
            _dataLoader = dataLoader;
        }

        public SolverResult Solve(string filename)
        {
            var input = _dataLoader.LoadInputForJackson(filename);

            return new SolverResult
            {
                OutputData = new Output { ChannelsCount = (int)input.Lambda }
            };
        }
    }
}
