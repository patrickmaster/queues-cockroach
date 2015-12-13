using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Queue.Algorithm.Data;

namespace Queue.ConsoleUI.Data
{
    class XmlDataLoader : IFileDataLoader
    {
        public Input LoadInputForJackson(string filename)
        {
            throw new NotImplementedException();
        }

        public BcmpInput LoadInputForBcmp(string filename)
        {
            throw new NotImplementedException();
        }
    }

    internal interface IFileDataLoader
    {
        Input LoadInputForJackson(string filename);

        BcmpInput LoadInputForBcmp(string filename);
    }
}
