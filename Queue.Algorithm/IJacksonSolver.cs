using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Queue.Algorithm.Data;

namespace Queue.Algorithm
{
    public interface IJacksonSolver
    {
        Output Solve(IJacksonDataProvider dataProvider);
    }

    class JacksonSolver : IJacksonSolver
    {
        public Output Solve(IJacksonDataProvider dataProvider)
        {
            throw new NotImplementedException();
        }
    }
}
