using System;
using System.Collections.Generic;
using System.Text;
using CoreCI.Common.IoC.Interfaces;

namespace CoreCI.Modules.WebSockets
{
    internal static class WebSocketContainerExtension
    {
        public static IContainer AddWebSocketManager(this IContainer container)
        {
            return container;
        }
    }
}
