using Sample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Keywords
{
    class EnumSample : Executor
    {
        [TestMethod]
        public void Test_GetEnumList() 
        {
            Color[] colors = (Color[])Enum.GetValues(typeof(Color));
            Console.WriteLine("Value\tSymbol\tn----\t-----");
            foreach (var c in colors)
                Console.WriteLine("{0,5:D}\t{0:G}", c);
        }
    }


    
}
