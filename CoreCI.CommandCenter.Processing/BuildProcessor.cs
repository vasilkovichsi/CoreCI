using System;
using CoreCI.Common.Processors;

namespace CoreCI.CommandCenter.Processing
{
    internal class BuildProcessor : IProcessor
    {
        public void Run()
        {
            Console.WriteLine("BuildProcessor activated");
        }

        public void Terminate()
        {
            Console.WriteLine("BuildProcessor terminated");
        }
    }
}