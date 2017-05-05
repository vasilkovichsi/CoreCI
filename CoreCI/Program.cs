using CoreCI.Common.IoC;
using CoreCI.Common.IoC.Interfaces;
using CoreCI.Common.Modularity.Interfaces;

namespace CoreCI.CommandCenter
{
    class Program
    {
        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            IContainer container = new Container();
            IBootstrapper bootstrapper = new Common.Bootstrapper();
            bootstrapper.InitializeContainer(container);
            container.BuildServiceProvider();
            container.GetService<IModulesLoader>().InitializeModules("CommandCenter");
        }
    }
}