using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace Queue.ConsoleUI.DI
{
    static class Dependency
    {
        public static IContainer Register()
        {
            var builder = new ContainerBuilder();

            builder
                .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsImplementedInterfaces();

            var resolver = new Resolver();
            builder.Register<IResolver>(context => resolver);

            Algorithm.DI.DependencyConfig.Register(builder);

            var container = builder.Build();
            resolver.Container = container;

            return container;
        }
    }
}
