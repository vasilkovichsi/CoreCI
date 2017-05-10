using System;

namespace CoreCI.CommandCenter.Logic.Interfaces
{
    public interface IThread
    {
        void Start(object o);
    }

    internal class SupportThread : IThread
    {
        public void Start(object o)
        {
            while (true)
            {
                Console.WriteLine("support thread ");
            }
        }
    }
}