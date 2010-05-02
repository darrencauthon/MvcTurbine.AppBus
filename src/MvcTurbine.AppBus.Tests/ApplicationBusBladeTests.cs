using System;
using System.Collections.Generic;
using AppBus;
using AutoMoq;
using Moq;
using MvcTurbine.ComponentModel;
using NUnit.Framework;

namespace MvcTurbine.AppBus.Tests
{
    [TestFixture]
    public class ApplicationBusBladeTests
    {
        private AutoMoqer mocker;

        [SetUp]
        public void TestSetup()
        {
            mocker = new AutoMoqer();
        }

        [Test]
        public void Initialize_should_register_the_application_bus()
        {
            var serviceLocatorFake = new ServiceLocatorFake();
            var contextFake = GetContextFakeThatReturnsThisServiceLocator(serviceLocatorFake);

            var blade = CreateTheApplicationBusBlade();
            blade.Initialize(contextFake.Object);

            Assert.IsNotNull(serviceLocatorFake.RegisteredApplicationBus);
        }

        [Test]
        public void When_one_handler_exists_then_it_is_added_to_the_application_bus()
        {
            var messageHandlerType = CreateMessageHandlerType();
            var messageHandlerRetrieverFake = CreateMessageHandlerRetrieverFake(new[]{messageHandlerType});

            var applicationBusFake = new Mock<IApplicationBus>();
            var contextFake = CreateContextFakeThatUsesThisApplicationBus(applicationBusFake);

            var blade = CreateTheApplicationBusBlade();
            blade.MessageHandlerRetriever = messageHandlerRetrieverFake.Object;
            blade.Spin(contextFake.Object);

            applicationBusFake.Verify(x => x.Add(messageHandlerType), Times.Once());
        }

        [Test]
        public void The_default_MessageHandlerRetriever_is_set_to_instance_of_MessageHandlerRetriever()
        {
            var serviceLocatorFake = new ServiceLocatorFake();
            var contextFake = GetContextFakeThatReturnsThisServiceLocator(serviceLocatorFake);

            var blade = CreateTheApplicationBusBlade();
            blade.Initialize(contextFake.Object);

            Assert.IsInstanceOf(typeof (MessageHandlerRetriever), blade.MessageHandlerRetriever);
        }

        private Mock<IRotorContext> CreateContextFakeThatUsesThisApplicationBus(Mock<IApplicationBus> applicationBusFake)
        {
            var serviceLocatorFake = new ServiceLocatorFake();
            serviceLocatorFake.RegisteredApplicationBus = applicationBusFake.Object;
            return GetContextFakeThatReturnsThisServiceLocator(serviceLocatorFake);
        }

        private Type CreateMessageHandlerType()
        {
            return new Mock<IMessageHandler>().Object.GetType();
        }

        private Mock<IMessageHandlerRetriever> CreateMessageHandlerRetrieverFake(Type[] messageHandlerTypes)
        {
            var messageHandlerRetrieverFake = mocker.GetMock<IMessageHandlerRetriever>();
            messageHandlerRetrieverFake.Setup(x => x.GetAllMessageHandlerTypes())
                .Returns(messageHandlerTypes);
            return messageHandlerRetrieverFake;
        }

        private ApplicationBusBlade CreateTheApplicationBusBlade()
        {
            return mocker.Resolve<ApplicationBusBlade>();
        }

        private Mock<IRotorContext> GetContextFakeThatReturnsThisServiceLocator(ServiceLocatorFake serviceLocatorFake)
        {
            var contextFake = new Mock<IRotorContext>();
            contextFake.Setup(x => x.ServiceLocator)
                .Returns(serviceLocatorFake);
            return contextFake;
        }

        #region servicelocatorfake

        public class ServiceLocatorFake : IServiceLocator
        {
            public IApplicationBus RegisteredApplicationBus { get; set; }

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public T Resolve<T>() where T : class
            {
                if (typeof (T) == typeof (IMessageHandlerRetriever))
                    return new MessageHandlerRetriever() as T;

                return RegisteredApplicationBus as T;
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
                throw new NotImplementedException();
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
                RegisteredApplicationBus = instance as IApplicationBus;
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