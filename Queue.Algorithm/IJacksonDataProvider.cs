using Queue.Algorithm.Data;

namespace Queue.Algorithm
{
    public interface IJacksonDataProvider
    {
        JacksonInput GetInput(string filename);
    }
}