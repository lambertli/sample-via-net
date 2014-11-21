using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Entities
{
    public class Dog : Animal
    {
        public Dog(string name) 
        {
            this.Name = name;
        }

        public override string Name
        {
            get;
            set;
        }

        public override AnimalType AnimalType
        {
            get { return AnimalType.Dog; }
        }

        public override void Speak()
        {
            Console.WriteLine("I'm a {0}, my name is {1}, meow meow..", AnimalType.ToString(), Name);
        }
    }
}
