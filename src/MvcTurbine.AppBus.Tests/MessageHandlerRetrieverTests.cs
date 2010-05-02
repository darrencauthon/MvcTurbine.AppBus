using System;
using System.Linq;
using AppBus;
using AutoMoq;
using NUnit.Framework;

namespace MvcTurbine.AppBus.Tests
{
    [TestFixture]
    public class MessageHandlerRetrieverTests
    {
        private AutoMoqer mocker;

        [SetUp]
        public void TestSetup()
        {
            mocker = new AutoMoqer();
        }

        [Test]
        public void Should_return_all_message_handlers_in_the_current_domain()
        {
            var retriever = mocker.Resolve<MessageHandlerRetriever>();

            var results = retriever.GetAllMessageHandlerTypes();

            Assert.AreEqual(5, results.Count());
            Assert.Contains(typeof (Test1), results.ToList());
            Assert.Contains(typeof (Test2), results.ToList());
        }

        public class Test1 : IMessageHandler
        {
            public bool CanHandle(Type type)
            {
                throw new NotImplementedException();
            }

            public void Handle(object message)
            {
                throw new NotImplementedException();
            }
        }

        public class Test2 : IMessageHandler
        {
            public bool CanHandle(Type type)
            {
                throw new NotImplementedException();
            }

            public void Handle(object message)
            {
                throw new NotImplementedException();
            }
        }
    }
}