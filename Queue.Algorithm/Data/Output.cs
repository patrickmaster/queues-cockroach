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
        
        public IEnumerable<SystemParameters> SystemStats { get; set; }
        
        public int[] Channels { get; set; }
        
        public double Value { get; set; }
    }
}
