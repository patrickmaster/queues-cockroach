using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Queue.Algorithm;
using Queue.Algorithm.Data;

namespace Queue.ConsoleUI.DataLoading
{
    class BcmpDataProvider : IBcmpDataProvider
    {
        private readonly string _filename;
        private readonly IBcmpFileDataLoader _dataLoader;

        public BcmpDataProvider(string filename, IBcmpFileDataLoader dataLoader)
        {
            _filename = filename;
            _dataLoader = dataLoader;
        }

        public BcmpInput GetInput()
        {
            return _dataLoader.LoadInputForBcmp(_filename);
        }
    }
}
