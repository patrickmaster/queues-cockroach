namespace Queue.Algorithm.Cockroach
{
    internal interface IValueSolver<T>
    {
        double GetValue(T input);
        T GetRandomValue();
    }
}