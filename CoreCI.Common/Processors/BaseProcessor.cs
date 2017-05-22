using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCI.Common.Processors
{
    public abstract class BaseProcessor : IProcessor
    {
        protected bool IsRunning { get; set; }

        public virtual void Run()
        {
            while (IsRunning)
            {
                
            }
        }

        public virtual void Terminate()
        {
            IsRunning = false;
        }
    }
}
