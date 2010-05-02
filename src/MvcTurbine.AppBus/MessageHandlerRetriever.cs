using System;
using System.Collections.Generic;

namespace MvcTurbine.AppBus
{
    public class MessageHandlerRetriever : IMessageHandlerRetriever
    {
        public IEnumerable<Type> GetAllMessageHandlerTypes()
        {
            throw new NotImplementedException();
        }
    }
}