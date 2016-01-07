using System.Collections.Generic;
using Queue.Algorithm.Data;

namespace Queue.Algorithm
{
    internal interface IParametersSolver
    {
        IEnumerable<SystemStatistics> Solve(Input data, double[] m);
    }

    class ParametersSolver : IParametersSolver
    {
        public IEnumerable<SystemStatistics> Solve(Input data, double[] m)
        {
            throw new System.NotImplementedException();
        }
    }
}