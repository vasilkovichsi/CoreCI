using System;
using CoreCI.Common.Processors;

namespace CoreCI.Builder.Logic.Processors
{
    internal class Processor : IProcessor
    {
        public void Run()
        {
            Console.WriteLine("DoSomething"); 
        }

        public void Terminate()
        {
            Console.WriteLine("Terminated");
        }
    }
}