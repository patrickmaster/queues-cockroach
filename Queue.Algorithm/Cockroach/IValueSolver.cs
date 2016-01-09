namespace Queue.Algorithm.Cockroach
{
    internal interface IValueSolver<in T>
    {
        double GetValue(T input);
    }
}