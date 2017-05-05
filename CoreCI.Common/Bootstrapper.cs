﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CoreCI.Common.IoC.Interfaces;
using CoreCI.Common.Modularity;
using CoreCI.Common.Modularity.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CoreCI.Common
{
    public class Bootstrapper : IBootstrapper
    {
        public void InitializeContainer(IContainer container)
        {
            container.Register<IModulesLoader, ModulesLoader>();
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("config.json");
            IConfigurationRoot config = configurationBuilder.Build();

            container.AddOptions(config);
            container.Configure<ConfigModel>(config);
            container.BuildServiceProvider();

            container.Register<IModulesLoader, ModulesLoader>();
            var tmp = container.GetService<IModulesLoader>();
            tmp.InitializeModules("app1");
        }
    }
}
