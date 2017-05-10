﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Loader;
using CoreCI.Common.IoC.Interfaces;
using CoreCI.Common.Modularity.Interfaces;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.Options;

namespace CoreCI.Common.Modularity
{
    /// <summary>
    /// Resolve and register application modules.
    /// </summary>
    /// <seealso cref="CoreCI.Common.Modularity.Interfaces.IModulesLoader" />
    public class ModulesLoader : IModulesLoader
    {
        private readonly IContainer _container;
        private readonly IOptions<ConfigModel> _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModulesLoader"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="options">The options.</param>
        public ModulesLoader(IContainer container, IOptions<ConfigModel> options)
        {
            _container = container;
            _options = options;
        }

        /// <summary>
        /// Initialize modules from config by provided application type.
        /// </summary>
        /// <param name="appType">Type of application.</param>
        /// <exception cref="System.ArgumentException">Argument shouldn't be empty or null - appType</exception>
        /// <exception cref="System.DllNotFoundException"></exception>
        public void InitializeModules(string appType)
        {
            if (string.IsNullOrEmpty(appType))
            {
                throw new ArgumentException("Argument shouldn't be empty or null", nameof(appType));
            }

            IEnumerable<Module> moduleMetadatas = _options.Value.Modules.ModuleList;
            foreach (Module moduleMetadata in moduleMetadatas)
            {
                if (moduleMetadata.Application.Equals(appType, StringComparison.CurrentCultureIgnoreCase))
                {
                    Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath($@"{ApplicationEnvironment.ApplicationBasePath}\Modules\{moduleMetadata.Type}.dll");

                    if (assembly != null)
                    {
                        try
                        {
                            Type moduleType = assembly.GetType(moduleMetadata.Initializer);
                            IModule module = (IModule) Activator.CreateInstance(moduleType);
                            module?.InitializeModule(_container);
                        }
                        catch (Exception ex)
                        {
                            //Log.Error(ex);
                            throw;
                        }
                    }
                    else
                    {
                        throw new DllNotFoundException(moduleMetadata.Name);
                    }
                }
            }
            _container.BuildServiceProvider();
        }
    }
}