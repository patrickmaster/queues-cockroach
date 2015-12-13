using System.Collections.Generic;

namespace Queue.Algorithm.Data
{
    public class BcmpInput
    {
        public IEnumerable<Input> Classes { get; set; }

        public IEnumerable<SystemType> Types { get; set; }
    }

    public enum SystemType
    {
        One,
        TheOther
    }
}
