using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.DesignPatterns
{
    class ObserverSample : Executor
    {
        Subject subject = new NewsPaperOffer("人民日报");
        Observer[] readers = new NewsReader[3]
        {
            new NewsReader("小明"),
            new NewsReader("小红"),
            new NewsReader("小李")
        };

        public ObserverSample() 
        {
            methodHandler += Test_Sample_Attach;
            methodHandler += Test_Sample_Detach;

        }

        public void Test_Sample_Attach() 
        {
            Console.WriteLine("Add three newspaper readers");
            foreach(var reader in readers)
                subject.Attach(reader);

            subject.Notify();
        }

        public void Test_Sample_Detach()
        {
            Console.WriteLine("Remove reader 小李");
            subject.Detach(readers[2]);
            subject.Notify();
        }

        /// <summary>
        /// 观察者
        /// </summary>
        abstract class Observer
        {
            public abstract void Update();
        }

        /// <summary>
        /// 发布者
        /// </summary>
        abstract class Subject
        {
            private IList<Observer> _observers = new List<Observer>();

            public void Attach(Observer observer)
            {
                _observers.Add(observer);
            }

            public void Detach(Observer observer)
            {
                _observers.Remove(observer);
            }

            public void Notify()
            {
                foreach (var observer in _observers)
                    observer.Update();
            }
        }

        /// <summary>
        /// 报社
        /// </summary>
        class NewsPaperOffer : Subject
        {
            public NewsPaperOffer(string name)
            {
                this.Name = name;
            }

            public string Name { get; set; }
        }

        /// <summary>
        /// 订报者
        /// </summary>
        class NewsReader : Observer
        {
            public NewsReader(string name)
            {
                this.Name = name;
            }

            public string Name { get; set; }

            public override void Update()
            {
                Console.WriteLine("hi {0}, have newspaper", this.Name);
            }
        }
    }
}
