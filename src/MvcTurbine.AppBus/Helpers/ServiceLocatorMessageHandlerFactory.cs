using System;
using AppBus;
using MvcTurbine.ComponentModel;

namespace MvcTurbine.AppBus.Helpers
{
    public class ServiceLocatorMessageHandlerFactory : IMessageHandlerFactory
    {
        private readonly IServiceLocator serviceLocator;

        public ServiceLocatorMessageHandlerFactory(IServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
        }

        public IMessageHandler Create(Type type)
        {
            return serviceLocator.Resolve(type) as IMessageHandler;
        }
    }
}