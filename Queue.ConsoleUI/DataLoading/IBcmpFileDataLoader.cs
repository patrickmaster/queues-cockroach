using Queue.Algorithm.Data;

namespace Queue.ConsoleUI.DataLoading
{
    internal interface IBcmpFileDataLoader
    {
        BcmpInput LoadInputForBcmp(string filename);
    }
}