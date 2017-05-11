using System.IO;
using CoreCI.Common.IoC;
using CoreCI.Common.IoC.Interfaces;
using CoreCI.Common.Logging;
using CoreCI.Common.Logging.Interfaces;
using CoreCI.Common.Modularity;
using CoreCI.Common.Modularity.Interfaces;
using Microsoft.Extensions.Configuration;

namespace CoreCI.Common
{
    public class Bootstrapper : IBootstrapper
    {
        public void InitializeContainer(IContainer container)
        {
            container.Register<IModulesLoader, ModulesLoader>();
            container.Register<ILogger, Log4NetLogger>(LifeTimeManager.ContainerControlled);
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("config.json");
            IConfigurationRoot config = configurationBuilder.Build();

            container.AddOptions(config);
            container.Configure<ConfigModel>(config);
            container.BuildServiceProvider();

            container.Register<IModulesLoader, ModulesLoader>();
        }
    }
}
