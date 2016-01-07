using Queue.Algorithm;
using Queue.Algorithm.Data;

namespace Queue.ConsoleUI.DataLoading
{
    class BcmpDataProvider : IBcmpDataProvider
    {
        private readonly IBcmpFileDataLoader _dataLoader;

        public BcmpDataProvider(IBcmpFileDataLoader dataLoader)
        {
            _dataLoader = dataLoader;
        }

        public BcmpInput GetInput(string filename)
        {
            return _dataLoader.LoadInputForBcmp(filename);
        }
    }
}
