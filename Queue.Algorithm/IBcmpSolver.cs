using System;
using Queue.Algorithm.Data;

namespace Queue.Algorithm
{
    public interface IBcmpSolver
    {
        Output Solve(BcmpInput input);
    }

    internal class BcmpSolver : IBcmpSolver
    {
        public Output Solve(BcmpInput input)
        {
            throw new NotImplementedException();
        }
    }
}
