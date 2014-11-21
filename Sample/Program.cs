using Sample.DesignPatterns;
using Sample.Framework45;
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
                new SingletonSample(),
                new ObserverSample(),
                new SystemLazySample()
            };

            foreach(var executor in executors)
                executor.Run();

            Console.ReadKey();
        }
    }
}
