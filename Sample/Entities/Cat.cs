using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Entities
{
    public delegate void ScreamHandler(object sender);

    public class Cat : Animal
    {
        public ScreamHandler ScreamHandler = null;
        
        public Cat(string name) 
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
            get
            {
                return AnimalType.Cat;
            }
        }

        public override void Speak()
        {
            Console.WriteLine("I'm a {0}, my name is {1}, meow meow..", AnimalType.ToString(), Name);
        }

        public void Scream() 
        {
            Console.WriteLine("Meow meow..");
            if (this.ScreamHandler != null)
                this.ScreamHandler(this);
        }

        public void Scream(params Action[] actions) 
        {
            Console.WriteLine("Meow meow..");
            foreach (var action in actions)
                action();
        }
    }
}
