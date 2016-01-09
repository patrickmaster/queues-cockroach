namespace Queue.Algorithm.Cockroach
{
    internal interface IRandomizer<out T>
    {
        T GetValue();
    }
}