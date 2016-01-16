using Queue.Algorithm;
using Queue.Algorithm.Data;

namespace Queue.ConsoleUI.DataLoading
{
    class JacksonDataProvider : IJacksonDataProvider
    {
        private readonly IJacksonFileDataLoader _dataLoader;

        public JacksonDataProvider(IJacksonFileDataLoader dataLoader)
        {
            _dataLoader = dataLoader;
        }

        public JacksonInput GetInput(string filename)
        {
            return _dataLoader.LoadInputForJackson(filename);
        }
    }
}
