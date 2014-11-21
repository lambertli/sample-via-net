using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        //override Console object
        public static class Console
        {
            public static void Write(string msg, params object[] args)
            {
                System.Console.Write(msg, args);
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
