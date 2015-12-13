using Queue.Algorithm.Data;

namespace Queue.ConsoleUI.DataLoading
{
    interface IBcmpFileDataLoader
    {
        BcmpInput LoadInputForBcmp(string filename);
    }

    class BcmpFileDataLoader : IBcmpFileDataLoader
    {
        public BcmpInput LoadInputForBcmp(string filename)
        {
            throw new System.NotImplementedException();
        }
    }
}