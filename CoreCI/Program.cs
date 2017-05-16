using System;
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
            IBootstrapper commonBootstrapper = new Common.Bootstrapper();
            commonBootstrapper.InitializeContainer(container);

            IBootstrapper bootstrapper = new Bootstrapper();
            bootstrapper.InitializeContainer(container);

            container.BuildServiceProvider();
            container.GetService<IModulesLoader>().InitializeModules("CommandCenter");

            IEnumerable<IProcessor> processors = container.GetService<IProcessorsLoader>().InitializeProcessors();
            foreach (IProcessor processor in processors)
            {
                Thread thread = new Thread(() =>
                {
                    try
                    {
                        processor.Run();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                });
                thread.Start();
                _processors.Add(processor, thread);
            }

            Console.ReadKey();
            foreach (KeyValuePair<IProcessor, Thread> processor in _processors)
            {
                processor.Key.Terminate();
                processor.Value.Join();
            }
            Console.ReadKey();
        }
    }
}