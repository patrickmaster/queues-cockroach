using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace Queue.ConsoleUI.DI
{
    class Dependency
    {
        public static IContainer Register()
        {
            var builder = new ContainerBuilder();

            builder
                .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsImplementedInterfaces();

            Algorithm.DI.DependencyConfig.Register(builder);

            return builder.Build();
        }
    }
}
