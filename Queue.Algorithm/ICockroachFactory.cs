using System;
using Queue.Algorithm.Cockroach;
using Queue.Algorithm.Data;

namespace Queue.Algorithm
{
    internal interface ICockroachFactory
    {
        ICockroach<int[]> GetCockroach(double[] mi, double[] lambda);
        ICockroach<int[]> GetCockroach(BcmpInput input, double[][] lambda);
    }

    class CockroachFactory : ICockroachFactory
    {
        private readonly IJacksonParametersSolver _jacksonParametersSolver;
        private readonly IBcmpParametersSolver _bcmpParametersSolver;

        public CockroachFactory(IJacksonParametersSolver jacksonParametersSolver, IBcmpParametersSolver bcmpParametersSolver)
        {
            _jacksonParametersSolver = jacksonParametersSolver;
            _bcmpParametersSolver = bcmpParametersSolver;
        }

        public ICockroach<int[]> GetCockroach(double[] mi, double[] lambda)
        {
            if (mi == null) throw new ArgumentNullException("mi");
            if (lambda == null) throw new ArgumentNullException("lambda");

            var cockroach = new JacksonQueueCockroach(_jacksonParametersSolver, mi, lambda);
            return cockroach;
        }

        public ICockroach<int[]> GetCockroach(BcmpInput input, double[][] lambda)
        {
            if (input == null) throw new ArgumentNullException("input");
            if (lambda == null) throw new ArgumentNullException("lambda");

            var cockroach = new BcmpQueueCockroach(_bcmpParametersSolver, input.Mi, lambda, input.Type);
            return cockroach;
        }
    }
}