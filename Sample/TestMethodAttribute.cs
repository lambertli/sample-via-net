using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TestMethodAttribute : Attribute
    {
        public object[] Arguments { get; set; }

        public TestMethodAttribute() { }

        public TestMethodAttribute(params object[] args) 
        {
            this.Arguments = args;
        }
    }
}
