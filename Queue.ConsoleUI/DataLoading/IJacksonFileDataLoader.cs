using Queue.Algorithm.Data;

namespace Queue.ConsoleUI.DataLoading
{
    internal interface IJacksonFileDataLoader
    {
        Input LoadInputForJackson(string filename);
    }
}