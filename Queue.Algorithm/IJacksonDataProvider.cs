using Queue.Algorithm.Data;

namespace Queue.Algorithm
{
    public interface IJacksonDataProvider
    {
        Input GetInput(string filename);
    }
}