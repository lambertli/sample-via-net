using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Entities
{
    public abstract class Animal : SampleEntity
    {
        public abstract string Name { get; set; }
        public abstract AnimalType AnimalType { get; }
        public abstract void Speak();
    }

    public enum AnimalType 
    {
        Cat,
        Dog,
        Mouse
    }
}
