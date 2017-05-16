using System;
using System.Collections.Generic;
using System.Text;
using CoreCI.Common.Processors;

namespace CoreCI.CommandCenter.Processing
{
    internal class ServiceProcessor : IProcessor
    {
        public void Run()
        {
            Console.WriteLine("ServiceProcessor activated");
        }

        public void Terminate()
        {
            Console.WriteLine("ServiceProcessor terminated");
        }
    }
}
