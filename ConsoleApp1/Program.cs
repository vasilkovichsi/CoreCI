using System;
using CoreCI.Modules.Test;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            TestModule tm = new TestModule();
            Type type = tm.GetType();
            Console.WriteLine("Hello World!");
        }
    }
}