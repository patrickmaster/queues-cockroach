using Queue.Algorithm.Data;

namespace Queue.ConsoleUI.DataLoading
{
    internal interface IJacksonFileDataLoader
    {
        JacksonInput LoadInputForJackson(string filename);
    }
}