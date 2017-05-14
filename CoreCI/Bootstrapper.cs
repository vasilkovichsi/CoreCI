using System.IO;
using CoreCI.Common.IoC.Interfaces;
using CoreCI.Common.Modularity;
using CoreCI.Common.Processors;
using Microsoft.Extensions.Configuration;

namespace CoreCI.CommandCenter
{
    public class Bootstrapper : IBootstrapper
    {
        public void InitializeContainer(IContainer container)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("config.json").AddJsonFile("processors.json");
            IConfigurationRoot config = configurationBuilder.Build();

            container.AddOptions(config);
            container.Configure<ConfigModel>(config);
            container.Configure<ProcessorsConfigModel>(config);
            container.BuildServiceProvider();
        }
    }
}
