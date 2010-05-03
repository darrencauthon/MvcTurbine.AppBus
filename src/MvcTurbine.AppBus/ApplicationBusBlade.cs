using System;
using System.Collections.Generic;
using AppBus;
using MvcTurbine.AppBus.Helpers;
using MvcTurbine.Blades;
using MvcTurbine.ComponentModel;

namespace MvcTurbine.AppBus
{
    public class ApplicationBusBlade : Blade
    {
        public override void Initialize(IRotorContext context)
        {
        }

        public override void Spin(IRotorContext context)
        {
            SetupTheApplicationBus(context);

            var applicationBus = GetTheApplicationBus(context);
            var types = GetTypesToRegister(context);

            types.ForEach(applicationBus.Add);
        }

        private IEnumerable<Type> GetTypesToRegister(IRotorContext context)
        {
            var messageHandlerRetriever = GetMessageHandlerRetriever(context);
            return messageHandlerRetriever.GetAllMessageHandlerTypes();
        }

        private IMessageHandlerRetriever GetMessageHandlerRetriever(IRotorContext context)
        {
            IMessageHandlerRetriever messageHandlerRetriever;
            try
            {
                messageHandlerRetriever = context.ServiceLocator.Resolve<IMessageHandlerRetriever>();
            }
            catch (ServiceResolutionException exception)
            {
                messageHandlerRetriever = new MessageHandlerRetriever();
            }
            return messageHandlerRetriever;
        }

        private IApplicationBus GetTheApplicationBus(IRotorContext context)
        {
            var serviceLocator = context.ServiceLocator;
            return serviceLocator.Resolve<IApplicationBus>();
        }

        private static void SetupTheApplicationBus(IRotorContext context)
        {
            var applicationBus = CreateApplicationBus(context);
            RegisterThisApplicationBusInstanceWithTheServiceLocator(context, applicationBus);
        }

        private static void RegisterThisApplicationBusInstanceWithTheServiceLocator(IRotorContext context, ApplicationBus applicationBus)
        {
            var serviceLocator = context.ServiceLocator;
            serviceLocator.Register<IApplicationBus>(applicationBus);
        }

        private static ApplicationBus CreateApplicationBus(IRotorContext context)
        {
            var serviceLocator = context.ServiceLocator;
            var serviceLocatorMessageHandlerFactory = new ServiceLocatorMessageHandlerFactory(serviceLocator);
            return new ApplicationBus(serviceLocatorMessageHandlerFactory);
        }

        public void AddRegistrations(AutoRegistrationList registrationList)
        {
        }
    }
}