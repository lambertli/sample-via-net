using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Keywords
{
    class DynamicSample : Executor
    {
        [TestMethod]
        public void Test_Sample_1()
        {
            dynamic a = 123;
            dynamic b = 456;
            Console.WriteLine(Add(a,b));

            a = "123";
            b = "456";
            Console.WriteLine(Add(a, b));
        
            Console.WriteLine(a.Contains("2"));
        }

        [TestMethod]
        public void Test_Sample_2() 
        {
            dynamic e = new ExpandoObject();
            e.x = 6;
            e.y = "Hello";
            e.z = null;

            foreach (var kv in (IDictionary<string, object>)e)
                Console.WriteLine("Key={0} Value={1}", kv.Key, kv.Value);

        }


        private int Add(int a, int b) 
        {
            return a + b;
        }

        private string Add(string a, string b) 
        {
            return a + b;
        }
    }
}
