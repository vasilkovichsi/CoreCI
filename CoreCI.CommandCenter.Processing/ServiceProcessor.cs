using System;
using System.Collections.Generic;
using System.Text;
using CoreCI.Common.Communication;
using CoreCI.Common.Processors;

namespace CoreCI.CommandCenter.Processing
{
    internal class ServiceProcessor : BaseProcessor, IProcessor
    {
        private readonly IMessenger _messageHandler;
        public ServiceProcessor(IMessenger messageHandler)
        {
            _messageHandler = messageHandler;
        }

        public override void Run()
        {
            Console.WriteLine("ServiceProcessor started");
        }

        public override void Terminate()
        {
            Console.WriteLine("ServiceProcessor terminated");
        }
    }
}
