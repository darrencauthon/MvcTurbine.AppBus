using System;
using System.Collections.Generic;

namespace MvcTurbine.AppBus
{
    public interface IMessageHandlerRetriever
    {
        IEnumerable<Type> GetAllMessageHandlerTypes();
    }
}