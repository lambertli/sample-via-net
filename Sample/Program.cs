using Sample.DesignPatterns;
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
                new Singleton()
            };

            foreach(var executor in executors)
                executor.Run();

            Console.ReadKey();
        }
    }
}
