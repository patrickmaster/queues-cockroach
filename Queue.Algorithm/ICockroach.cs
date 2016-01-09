namespace Queue.Algorithm
{
    internal interface ICockroach<out T>
    {
        T GetNext();
    }
}