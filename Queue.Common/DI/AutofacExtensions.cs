using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;

namespace Queue.ConsoleUI.DI
{
    static class AutofacExtensions
    {
        public static object ResolveUnregistered(this IComponentContext context, Type serviceType, IEnumerable<Parameter> parameters)
        {
            var scope = context.Resolve<ILifetimeScope>();
            using (var innerScope = scope.BeginLifetimeScope(b => b.RegisterType(serviceType)))
            {
                IComponentRegistration reg;
                innerScope.ComponentRegistry.TryGetRegistration(new TypedService(serviceType), out reg);

                return context.ResolveComponent(reg, parameters);
            }
        }
        public static object ResolveUnregistered(this IComponentContext context, Type serviceType)
        {
            return ResolveUnregistered(context, serviceType, Enumerable.Empty<Parameter>());
        }

        public static object ResolveUnregistered(this IComponentContext context, Type serviceType, params Parameter[] parameters)
        {
            return ResolveUnregistered(context, serviceType, (IEnumerable<Parameter>)parameters);
        }

        public static TService ResolveUnregistered<TService>(this IComponentContext context)
        {
            return (TService)ResolveUnregistered(context, typeof(TService), Enumerable.Empty<Parameter>());
        }

        public static TService ResolveUnregistered<TService>(this IComponentContext context, params Parameter[] parameters)
        {
            return (TService)ResolveUnregistered(context, typeof(TService), (IEnumerable<Parameter>)parameters);
        }
    }
}