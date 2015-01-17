using Sample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.DesignPatterns
{
    class DecoratorSample : Executor
    {
        [TestMethod]
        public void Test_Sample() 
        {
            Person xiaoli = new Person("小李");

            Sneakers sneakers = new Sneakers();
            BigTrouser bigTrouser = new BigTrouser();
            TShirts tshirts = new TShirts();

            Console.WriteLine("My first dress up");
            sneakers.Decorate(xiaoli);
            bigTrouser.Decorate(sneakers);
            tshirts.Decorate(bigTrouser);
            tshirts.ShowDress();
            Console.WriteLine();

            Console.WriteLine("My senond dress up");
            tshirts.Decorate(xiaoli);
            sneakers.Decorate(tshirts);
            bigTrouser.Decorate(sneakers);
            bigTrouser.ShowDress();
            Console.WriteLine();
        }
    }


    class Finery : Person 
    {
        protected Person component;

        public void Decorate(Person component) 
        {
            this.component = component;
        }

        public override void ShowDress()
        {
            if (this.component != null) 
            {
                component.ShowDress();
            }
        }
    }

    class TShirts : Finery 
    {
        public override void ShowDress()
        {
            base.ShowDress();
            Console.Write("T shirt\t");
        }
    }

    class BigTrouser : Finery 
    {
        public override void ShowDress()
        {
            base.ShowDress();
            Console.Write("Big trouser\t");
        }
    }

    class Sneakers : Finery 
    {
        public override void ShowDress()
        {
            base.ShowDress();
            Console.Write("Sneakers\t");
        }
    }
}
