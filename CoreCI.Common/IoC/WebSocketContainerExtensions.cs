using System.Reflection;
using CoreCI.Common.IoC.Interfaces;
using WebSocketManager;

namespace CoreCI.Common.IoC
{
    public static class WebSocketContainerExtensions
    {
        public static IContainer AddWebSocketManager(this IContainer container)
        {
            container.Register(typeof(WebSocketConnectionManager), LifeTimeManager.Transient);

            foreach (var type in Assembly.GetEntryAssembly().ExportedTypes)
            {
                if (type.GetTypeInfo().BaseType == typeof(WebSocketHandler))
                {
                    container.Register(type,LifeTimeManager.ContainerControlled);
                }
            }
            return container;
        }
    }
}