using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sample
{
    class Executor
    {
        protected delegate void TestMethodHandler();
        protected TestMethodHandler methodHandler = null;
        static Stopwatch watch = new Stopwatch();

        protected Executor() 
        {
            AttachTestMethod();
        }

        protected virtual void AttachTestMethod()
        {
            Type type = this.GetType();
            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Static);
            foreach (var method in methods)
            {
                var attr = method.GetCustomAttribute<TestMethodAttribute>();
                if (attr != null)
                {
                    methodHandler += () => 
                    {
                        Console.WriteLine("Begin runing method {0}", method.Name);
                        method.Invoke(this, attr.Arguments);
                    };
                }
            }
        }

        public virtual void Run()
        {
            watch.Restart();
            Console.WriteLine("Begin runing sample {0}...", this.GetType().Name);
            if (methodHandler != null)
            {
                methodHandler();
            }
            Console.WriteLine("Sample {0} executed!", this.GetType().Name);
            watch.Stop();
        }

        // Override Console.Write
        public static class Console
        {
            public static void Write(string msg, params object[] args)
            {
                System.Console.Write(msg, args);
            }

            public static void WriteLine() 
            {
                System.Console.WriteLine();
            }

            public static void WriteLine(object obj)
            {
                WriteLine("{0}", obj);
            }

            /// <summary>
            /// format with elapsed time and thread id
            /// elapsed millisecend|tread id - message
            /// </summary>
            /// <param name="msg"></param>
            /// <param name="args"></param>
            public static void WriteLine(string msg, params object[] args)
            {
                System.Console.WriteLine(string.Format("{0,7:N0}ms|{1,2} - {2}", watch.ElapsedMilliseconds, Thread.CurrentThread.ManagedThreadId, msg), args);
            }
        }
    }
}
