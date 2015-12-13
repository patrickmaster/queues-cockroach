using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Queue.Algorithm;
using Queue.Algorithm.Data;

namespace Queue.ConsoleUI.DataLoading
{
    class JacksonDataProvider : IJacksonDataProvider
    {
        private readonly string _filename;
        private readonly IJacksonFileDataLoader _dataLoader;

        public JacksonDataProvider(string filename, IJacksonFileDataLoader dataLoader)
        {
            _filename = filename;
            _dataLoader = dataLoader;
        }

        public Input GetInput()
        {
            return _dataLoader.LoadInputForJackson(_filename);
        }
    }
}
