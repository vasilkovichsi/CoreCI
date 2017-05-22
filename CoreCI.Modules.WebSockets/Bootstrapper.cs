using System;
using System.Collections.Generic;
using System.Text;
using CoreCI.Common.Communication;
using CoreCI.Common.IoC.Interfaces;
using CoreCI.Modules.WebSockets.Interfaces;
using WebSocketManager;

namespace CoreCI.Modules.WebSockets
{
    public class Bootstrapper : IBootstrapper
    {
        public void InitializeContainer(IContainer container)
        {
            IBootstrapper bootstrapper = new Common.Bootstrapper();
            bootstrapper.InitializeContainer(container);

            container.Register<IWebSocketMessageHandler, WebSocketMessageHandler>();
            container.Register(typeof(WebSocketMessageHandler));
            container.Register<IMessenger, WebSocketMessenger>();
        }
    }
}
