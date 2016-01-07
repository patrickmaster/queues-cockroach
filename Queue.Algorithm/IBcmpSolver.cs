using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Queue.Algorithm.Data;

namespace Queue.Algorithm
{
    public interface IBcmpSolver
    {
        Output Solve(IBcmpDataProvider dataProvider);
    }

    class BcmpOneSolver : IBcmpSolver
    {
        public Output Solve(IBcmpDataProvider dataProvider)
        {
            dataProvider.GetInput();
            throw new NotImplementedException();
        }
    }
}
