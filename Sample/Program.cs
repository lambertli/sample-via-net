using Sample.DesignPatterns;
using Sample.Framework45;
using Sample.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            IList<Executor> executors = new List<Executor> 
            {
                //new SingletonSample(),
                //new ObserverSample(),
                //new SystemLazySample(),
                new SystemReflectionSample(),
                //new DelegateSample()
            };

            foreach (var executor in executors)
            {
                executor.Run();
                Console.WriteLine("Enter key x to exit, other key to contiune...");
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.X)
                    break;
            }
        }
    }
}
