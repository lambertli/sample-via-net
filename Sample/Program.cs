﻿using Sample.DesignPatterns;
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
            List<Executor> executors = new List<Executor>();
            //Design pattern samples
            executors.AddRange(new List<Executor>()
            {
                //new ObserverSample(),
                //new SingletonSample()
            });
            //.net framework4.5 samples
            executors.AddRange(new List<Executor>()
            {
                //new SystemLazySample(),
                //new SystemReflectionSample()
            });
            //keyword samples
            executors.AddRange(new List<Executor>()
            {
                //new DelegateSample(),
                new DynamicSample()
            });

            foreach (var executor in executors)
            {
                executor.Run();
                Console.WriteLine("Enter key x to exit, other key to contiune...");
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.X)
                    break;

                Console.WriteLine();
            }
        }

    }
}
