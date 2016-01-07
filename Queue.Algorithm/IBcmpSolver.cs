using System;
using Queue.Algorithm.Data;

namespace Queue.Algorithm
{
    public interface IBcmpSolver
    {
        Output Solve(BcmpInput input);
    }

    class BcmpOneSolver : IBcmpSolver
    {
        public Output Solve(BcmpInput input)
        {
            throw new NotImplementedException();
        }
    }
}
