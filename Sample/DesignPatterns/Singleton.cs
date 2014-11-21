﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.DesignPatterns
{
    class Singleton : Executor
    {
        private int _threadCount = 10;

        public Singleton() 
        {
            base.methodHandler += this.Test_Sample_1;
            base.methodHandler += this.Test_Sample_2;
            base.methodHandler += this.Test_Sample_3;
            base.methodHandler += this.Test_Sample_4;
        }

        public void Test_Sample_1()
        {
            TestMultiThreadCreateInstance<MyClass1>(_threadCount);
        }

        public void Test_Sample_2()
        {
            TestMultiThreadCreateInstance<MyClass2>(_threadCount);
        }

        public void Test_Sample_3()
        {
            TestMultiThreadCreateInstance<MyClass3>(_threadCount);
        }

        public void Test_Sample_4()
        {
            TestMultiThreadCreateInstance<MyClass4>(_threadCount);
        }

        public void TestMultiThreadCreateInstance<T>(int threadCount) where T : class
        {
            Type t = typeof(T);
            Task[] tasks = new Task[threadCount];
            for (int i = 0; i < threadCount; i++)
            {
                tasks[i] = Task.Factory.StartNew(() =>
                {
                    t.InvokeMember("GetInstance", System.Reflection.BindingFlags.InvokeMethod, null, null, null);
                });
            }
            Task.WaitAll(tasks);
            Console.WriteLine("{0} Completed!", t.Name);
        }

        /// <summary>
        /// 饿汉型，在生成类时初始化，确保单例。
        /// </summary>
        public sealed class MyClass1
        {
            private static int instanceQuantity = 0;
            private static MyClass1 instance = new MyClass1();

            private MyClass1()
            {
                Thread.Sleep(3000);
                ++instanceQuantity;
                Console.WriteLine("Instance {0} created, current instance quantity is {1}", this.GetType().Name, instanceQuantity);
            }

            public static MyClass1 GetInstance()
            {
                Console.WriteLine("Begin geting instance {0}", typeof(MyClass1).Name);
                return instance;
            }
        }

        /// <summary>
        /// 懒汉型，需要时创建。多线程下不能确保单例
        /// </summary>
        public sealed class MyClass2
        {
            private static int instanceQuantity = 0;
            private static MyClass2 instance;

            private MyClass2()
            {
                Thread.Sleep(3000);
                ++instanceQuantity;
                Console.WriteLine("Instance {0} created, current instance quantity is {1}", this.GetType().Name, instanceQuantity);
            }

            public static MyClass2 GetInstance()
            {
                Console.WriteLine("Begin geting instance {0}", typeof(MyClass2).Name);
                if (instance == null)
                    instance = new MyClass2();

                return instance;
            }
        }


        /// <summary>
        /// 懒汉型加锁，需要时创建。能确保多线程下单例
        /// </summary>
        public sealed class MyClass3
        {
            private static int instanceQuantity = 0;
            private static MyClass3 instance;
            private static object locker = new object();

            private MyClass3()
            {
                Thread.Sleep(3000);
                ++instanceQuantity;
                Console.WriteLine("Instance {0} created, current instance quantity is {1}", this.GetType().Name, instanceQuantity);
            }

            public static MyClass3 GetInstance()
            {
                Console.WriteLine("Begin geting instance {0}", typeof(MyClass3).Name);
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                            instance = new MyClass3();
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// (推荐)懒汉型，确保多线程安全
        /// </summary>
        public sealed class MyClass4
        {
            private static int instanceQuantity = 0;
            private static Lazy<MyClass4> instance = instance = new Lazy<MyClass4>(() => { return new MyClass4(); });
            private MyClass4()
            {
                Thread.Sleep(3000);
                ++instanceQuantity;
                Console.WriteLine("Instance {0} created, current instance quantity is {1}", this.GetType().Name, instanceQuantity);
            }

            public static MyClass4 GetInstance()
            {
                Console.WriteLine("Begin geting instance {0}", typeof(MyClass4).Name);
                return instance.Value;
            }
        }
    }
}
