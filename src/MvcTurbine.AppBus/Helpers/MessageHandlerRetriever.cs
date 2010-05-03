using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AppBus;

namespace MvcTurbine.AppBus.Helpers
{
    public interface IMessageHandlerRetriever
    {
        IEnumerable<Type> GetAllMessageHandlerTypes();
    }

    public class MessageHandlerRetriever : IMessageHandlerRetriever
    {
        public IEnumerable<Type> GetAllMessageHandlerTypes()
        {
            var list = new List<Type>();

            foreach (var assembly in GetAllAssemblies())
                list.AddRange(GetAllMessageHandlersInThisAssembly(assembly));

            return list;
        }

        private static IEnumerable<Type> GetAllMessageHandlersInThisAssembly(Assembly assembly)
        {
            return assembly.GetTypes()
                .Where(x => x.GetInterfaces().Contains(typeof (IMessageHandler)));
        }

        private static IEnumerable<Assembly> GetAllAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => x.FullName.StartsWith("AppBus,") == false)
                .ToList();
        }
    }
}