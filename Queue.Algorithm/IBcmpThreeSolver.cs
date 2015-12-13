using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Queue.Algorithm.Data;

namespace Queue.Algorithm
{
    public interface IBcmpThreeSolver
    {
        Output Solve(IBcmpDataProvider dataProvider);
    }

    class BcmpThreeSolver : IBcmpThreeSolver
    {
        public Output Solve(IBcmpDataProvider dataProvider)
        {
            throw new NotImplementedException();
        }
    }
}
