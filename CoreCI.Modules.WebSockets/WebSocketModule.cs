using CoreCI.Common.IoC.Interfaces;
using CoreCI.Common.Modularity.Interfaces;

namespace CoreCI.Modules.WebSockets
{
    public class WebSocketModule : IModule
    {
        /// <summary>
        /// Initializes the module.
        /// </summary>
        /// <param name="container">The container.</param>
        public void InitializeModule(IContainer container)
        {
            IBootstrapper bootstrapper = new Bootstrapper();
            bootstrapper.InitializeContainer(container);
        }
    }
}
