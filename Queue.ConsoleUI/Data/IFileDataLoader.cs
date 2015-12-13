using Queue.Algorithm.Data;

namespace Queue.ConsoleUI.Data
{
    internal interface IFileDataLoader
    {
        Input LoadInputForJackson(string filename);

        BcmpInput LoadInputForBcmp(string filename);
    }
}