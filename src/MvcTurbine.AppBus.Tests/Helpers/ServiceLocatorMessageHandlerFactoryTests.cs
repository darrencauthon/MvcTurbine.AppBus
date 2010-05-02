using System;
using System.Collections.Generic;
using AppBus;
using AutoMoq;
using MvcTurbine.AppBus.Helpers;
using MvcTurbine.ComponentModel;
using NUnit.Framework;

namespace MvcTurbine.AppBus.Tests.Helpers
{
    [TestFixture]
    public class ServiceLocatorMessageHandlerFactoryTests
    {
        private AutoMoqer mocker;

        [SetUp]
        public void TestSetup()
        {
            mocker = new AutoMoqer();
        }

        [Test]
        public void Creating_message_handlers_goes_through_the_service_locator()
        {
            var expectedHandler = new FakeMessageHandler();

            var serviceLocator = CreateServiceLocatorThatReturnsThisHandlerType(expectedHandler);

            var factory = new ServiceLocatorMessageHandlerFactory(serviceLocator);

            var handler = factory.Create(typeof (FakeMessageHandler));

            Assert.AreSame(expectedHandler, handler);
        }

        private ServiceLocatorFake CreateServiceLocatorThatReturnsThisHandlerType(FakeMessageHandler expectedHandler)
        {
            var serviceLocator = new ServiceLocatorFake();
            serviceLocator.HandlerToReturn = expectedHandler;
            return serviceLocator;
        }

        public class EventMessage : IEventMessage
        {
        }

        public class FakeMessageHandler : MessageHandler<EventMessage>
        {
            public override void Handle(EventMessage message)
            {
                throw new NotImplementedException();
            }
        }

        #region ServiceLocatorFake

        public class ServiceLocatorFake : IServiceLocator
        {
            public FakeMessageHandler HandlerToReturn { get; set; }

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public T Resolve<T>() where T : class
            {
                return HandlerToReturn as T;
            }

            public T Resolve<T>(string key) where T : class
            {
                throw new NotImplementedException();
            }

            public T Resolve<T>(Type type) where T : class
            {
                throw new NotImplementedException();
            }

            public object Resolve(Type type)
            {
                if (type == typeof (FakeMessageHandler))
                    return HandlerToReturn;
                return null;
            }

            public IList<T> ResolveServices<T>() where T : class
            {
                throw new NotImplementedException();
            }

            public IServiceRegistrar Batch()
            {
                throw new NotImplementedException();
            }

            public void Register<Interface>(Type implType) where Interface : class
            {
                throw new NotImplementedException();
            }

            public void Register<Interface, Implementation>() where Implementation : class, Interface
            {
                throw new NotImplementedException();
            }

            public void Register<Interface, Implementation>(string key) where Implementation : class, Interface
            {
                throw new NotImplementedException();
            }

            public void Register(string key, Type type)
            {
                throw new NotImplementedException();
            }

            public void Register(Type serviceType, Type implType)
            {
                throw new NotImplementedException();
            }

            public void Register<Interface>(Interface instance) where Interface : class
            {
                throw new NotImplementedException();
            }

            public void Release(object instance)
            {
                throw new NotImplementedException();
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }

            public TService Inject<TService>(TService instance) where TService : class
            {
                throw new NotImplementedException();
            }

            public void TearDown<TService>(TService instance) where TService : class
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}