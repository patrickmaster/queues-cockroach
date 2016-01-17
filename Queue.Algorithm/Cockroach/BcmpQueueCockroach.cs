using System.Collections.Generic;
using Queue.Algorithm.Data;

namespace Queue.Algorithm.Cockroach
{
    internal class BcmpQueueCockroach : QueueCockroach
    {
        private readonly IBcmpParametersSolver _parametersSolver;
        private readonly double[][] _mi;
        private readonly double[][] _lambda;
        private readonly BcmpType[] _type;

        public BcmpQueueCockroach(IBcmpParametersSolver parametersSolver, double[][] mi, double[][] lambda, BcmpType[] type) 
            : base(mi.Length)
        {
            _parametersSolver = parametersSolver;
            _mi = mi;
            _lambda = lambda;
            _type = type;
        }

        protected override IEnumerable<SystemParameters> GetParameters(int[] state)
        {
            return _parametersSolver.GetParameters(state, _mi, _lambda, _type);
        }
    }
}