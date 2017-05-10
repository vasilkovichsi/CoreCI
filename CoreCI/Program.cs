using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using CoreCI.Common.IoC;
using CoreCI.Common.IoC.Interfaces;
using CoreCI.Common.Modularity.Interfaces;
using CoreCI.Common.Processors;

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
            IDictionary<IProcessor, Thread> _processors = new Dictionary<IProcessor, Thread>();
            IContainer container = new Container();
            IBootstrapper bootstrapper = new Common.Bootstrapper();
            bootstrapper.InitializeContainer(container);
            container.BuildServiceProvider();
            container.GetService<IModulesLoader>().InitializeModules("CommandCenter");

            IEnumerable<IProcessor> processors = container.GetServices<IProcessor>();
            foreach (IProcessor processor in processors)
            {
                _processors.Add(processor, new Thread(() =>
                {
                    try
                    {
                        processor.Run();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }));
            }

            Console.ReadKey();
            
        }
    }
}