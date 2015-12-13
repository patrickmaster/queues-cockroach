using Autofac;
using Queue.ConsoleUI.DI;

namespace Queue.Common.DI
{
    public interface IResolver
    {
        T Create<T>();
    }

    public class Resolver : IResolver
    {
        public IContainer Container { get; set; }
        
        public T Create<T>()
        {
            return Container.ResolveUnregistered<T>();
        }
    }
}
