using System;
using System.Linq;
using AppBus;
using AutoMoq;
using MvcTurbine.AppBus.Helpers;
using NUnit.Framework;

namespace MvcTurbine.AppBus.Tests.Helpers
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

            Assert.AreEqual(3, results.Count());
            Assert.Contains(typeof (Test1), results.ToList());
            Assert.Contains(typeof (Test2), results.ToList());
        }

        public class Test1 : IMessageHandler<Test1>
        {
            public void Handle(Test1 message)
            {
                throw new NotImplementedException();
            }
        }

        public class Test2 : IMessageHandler<Test2>
        {

            public void Handle(Test2 message)
            {
                throw new NotImplementedException();
            }
        }
    }
}