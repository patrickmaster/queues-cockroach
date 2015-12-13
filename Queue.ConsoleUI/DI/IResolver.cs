using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace Queue.ConsoleUI.DI
{
    interface IResolver
    {
        T Create<T>();
    }

    class Resolver : IResolver
    {
        public IContainer Container { get; internal set; }
        
        public T Create<T>()
        {
            return Container.ResolveUnregistered<T>();
        }
    }
}
