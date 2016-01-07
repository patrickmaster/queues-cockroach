using Queue.Algorithm.Data;

namespace Queue.Algorithm
{
    public interface IBcmpDataProvider
    {
        BcmpInput GetInput(string filename);
    }
}