using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Loader;
using CoreCI.Common.IoC.Interfaces;
using CoreCI.Common.Modularity.Interfaces;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.Options;

namespace CoreCI.Common.Modularity
{
    public class ModulesLoader : IModulesLoader
    {
        private readonly IContainer _container;
        private readonly IOptions<ConfigModel> _options;
        public ModulesLoader(IContainer container, IOptions<ConfigModel> options)
        {
            _container = container;
            _options = options;
        }

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