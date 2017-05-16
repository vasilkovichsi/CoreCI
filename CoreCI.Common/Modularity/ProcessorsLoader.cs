using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using CoreCI.Common.IoC.Interfaces;
using CoreCI.Common.Logging.Interfaces;
using CoreCI.Common.Modularity.Interfaces;
using CoreCI.Common.Processors;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.Options;

namespace CoreCI.Common.Modularity
{
    /// <summary>
    /// Loads processors for application.
    /// </summary>
    /// <seealso cref="IProcessorsLoader" />
    internal class ProcessorsLoader : IProcessorsLoader
    {
        private readonly IContainer _container;
        private readonly IOptions<ProcessorsConfigModel> _options;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessorsLoader"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="options">The options.</param>
        /// <param name="logger">The logger.</param>
        public ProcessorsLoader(IContainer container, IOptions<ProcessorsConfigModel> options, ILogger logger)
        {
            _container = container;
            _options = options;
            _logger = logger;
        }

        /// <summary>
        /// Initializes the processors.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.DllNotFoundException"></exception>
        public IList<IProcessor> InitializeProcessors()
        {
            IEnumerable<Processor> processors = _options.Value.Processors.ProcessorsList;
            IList<IProcessor> resolvedProcessors = new List<IProcessor>();
            foreach (Processor processor in processors)
            {
                Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath($@"{ApplicationEnvironment.ApplicationBasePath}\{processor.Assembly}.dll");

                if (assembly != null)
                {
                    try
                    {
                        Type moduleType = assembly.GetType(processor.Class);
                        if (moduleType != null)
                        {
                            _container.Register(moduleType);
                            resolvedProcessors.Add((IProcessor)Activator.CreateInstance(moduleType));
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogException(ex);
                        throw;
                    }
                }
                else
                {
                    throw new DllNotFoundException(processor.Assembly);
                }
            }
            return resolvedProcessors;
        }
    }
}
