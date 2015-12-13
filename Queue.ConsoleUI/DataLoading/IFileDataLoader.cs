using Queue.Algorithm.Data;

namespace Queue.ConsoleUI.DataLoading
{
    internal interface IFileDataLoader
    {
        Input LoadInputForJackson(string filename);

        BcmpInput LoadInputForBcmp(string filename);
    }
}