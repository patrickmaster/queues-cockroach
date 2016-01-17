using System.Collections.Generic;
using System.Linq;
using Queue.Algorithm.Data;

namespace Queue.Algorithm.Cockroach
{
    internal abstract class QueueCockroach : ArrayOfIntsCockroach
    {
        private const double WinFactor = 1;
        private const double LossFactor = 1;
        private const int MaxChannelsCount = 5;

        protected QueueCockroach(int length)
            : base(length, MaxChannelsCount)
        {
        }

        protected sealed override double GetValue(int[] state)
        {
            var parameters = GetParameters(state);
            return WinFactor * parameters.Sum(x => x.ServiceTime) - LossFactor * state.Sum();
        }

        protected abstract IEnumerable<SystemParameters> GetParameters(int[] state);
    }
}