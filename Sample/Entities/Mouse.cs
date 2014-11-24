using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Entities
{
    public class Mouse : Animal
    {
        public Mouse(string name) 
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
            get { return AnimalType.Mouse; }
        }

        public override void Speak()
        {
            Console.WriteLine("I'm a {0}, my name is {1}, zi zi..", AnimalType.ToString(), Name);
        }

        public void RunAway(object sender)
        {
            Cat c = sender as Cat;
            if (c != null)
                Console.WriteLine("{0} coming, {1} run away.", c.Name, this.Name);
        }
    }
}
