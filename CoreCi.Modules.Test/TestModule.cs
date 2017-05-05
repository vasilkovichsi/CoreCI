using System;
using CoreCI.Common.IoC.Interfaces;
using CoreCI.Common.Modularity.Interfaces;

namespace CoreCI.Modules.Test
{
    public class TestModule : IModule
    {
        public void InitializeModule(IContainer container)
        {
            Console.WriteLine("test");
        }
    }
}
