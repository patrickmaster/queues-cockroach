using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Algorithm.Data
{
    public class Output
    {
        public double Time { get; set; }

        public int ChannelsCount { get; set; }

        public double Value { get; set; }

        public IEnumerable<SystemStatistics> SystemStats { get; set; }
    }

    public class SystemStatistics
    {
        public double ServiceTime { get; set; }

        public double QueueTime { get; set; }

        public double AverageEntriesCount { get; set; }

        public double AverageQueueLength { get; set; }
    }
}
