using AppBus;
using MvcTurbine.Blades;

namespace MvcTurbine.AppBus
{
    public class ApplicationBusBlade : Blade
    {
        public IMessageHandlerRetriever MessageHandlerRetriever { get; set; }

        public override void Initialize(IRotorContext context)
        {
            SetupTheApplicationBus(context);
            SetupTheMessageHandlerRetriever(context);
        }

        private void SetupTheMessageHandlerRetriever(IRotorContext context)
        {
            MessageHandlerRetriever = context.ServiceLocator.Resolve<IMessageHandlerRetriever>();
        }

        public override void Spin(IRotorContext context)
        {
            var serviceLocator = context.ServiceLocator;
            var applicationBus = serviceLocator.Resolve<IApplicationBus>();

            var types = MessageHandlerRetriever.GetAllMessageHandlerTypes();
            foreach (var type in types)
                applicationBus.Add(type);
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
    }
}