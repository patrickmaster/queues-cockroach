using System;
using System.Collections.Generic;
using Queue.Algorithm.Data;

namespace Queue.Algorithm.Cockroach
{
    internal class JacksonQueueCockroach : QueueCockroach
    {
        private readonly IJacksonParametersSolver _jacksonParametersSolver;
        private readonly double[] _lambda;
        private readonly double[] _mi;

        public JacksonQueueCockroach(IJacksonParametersSolver jacksonParametersSolver, double[] mi, double[] lambda) 
            : base(mi.Length)
        {
            _jacksonParametersSolver = jacksonParametersSolver;
            _lambda = lambda;
            _mi = mi;

            if (mi.Length != lambda.Length)
                throw new ArgumentException("mi and lambda dimensions do not match");
        }

        protected override IEnumerable<SystemParameters> GetParameters(int[] state)
        {
            return _jacksonParametersSolver.SolveParameters(state, _mi, _lambda);
        }
    }
}