using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Entities
{
    public class Person : SampleEntity
    {
        public Person() { }
        public Person(string name) { this.Name = name; }

        public string Name { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public string PersonWebsiteUrl { get; set; }
        public bool IsDeleted { get; set; }

        public virtual void ShowDress()
        {
            Console.WriteLine("{0} showing dress up.", this.Name);
        }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
