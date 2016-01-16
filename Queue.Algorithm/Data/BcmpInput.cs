using System.Collections.Generic;

namespace Queue.Algorithm.Data
{
    public class BcmpInput
    {
        public double[] Lambda { get; set; }

        public double[][] Mi { get; set; }

        public double[][][] P { get; set; }

        public int K { get; set; }

        public BcmpType[] Type { get; set; }
    }

    public enum BcmpType
    {
        One = 1,
        Three = 3
    }
}
