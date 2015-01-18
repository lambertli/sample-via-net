using ClaySharp;
using Codeplex.Data;
using Newtonsoft.Json.Linq;
using Sample.Entities;
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
        string strPerson = "{\"Name\":\"macy\",\"Age\":29,\"Contact\":{\"photo\":13580656666,\"Address\":\"TH street no. 45\"}}";

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

        /// <summary>
        /// DynamicJson:http://dynamicjson.codeplex.com/
        /// </summary>
        [TestMethod]
        public void Test_Sample_3()
        {
            var obj = DynamicJson.Parse(strPerson);

            Console.WriteLine(obj.Name);
            Console.WriteLine(obj.Contact.Address);
            Console.WriteLine(obj);
        }

        [TestMethod]
        public void Test_Sample_4() 
        {
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(strPerson);

            Console.WriteLine(obj.Name);
            Console.WriteLine(obj.Contact.Address);
            Console.WriteLine(obj);
        }

        [TestMethod]
        public void Test_Sample_5() 
        {
            string[] selectValues = new string[2] { "Name", "Age" };
            var person = new Person { Name = "macy", Age = 25, Gender = Gender.Female };

            dynamic New = new ClayFactory();
            var dyPerson = New.Person();

            var properties = person.GetType().GetProperties();
            foreach (var property in properties) 
            {
                if (selectValues.Contains(property.Name)) 
                {
                    dyPerson[property.Name] = property.GetValue(person);
                }
            }

            Console.WriteLine(dyPerson.Name);
        }

        [TestMethod]
        public void Test_Sample_CustomerDynamic()
        {
            dynamic obj = new CustomerDynamicObject();
            obj.Name = "macy";
            Console.WriteLine(obj.Name);
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

    class CustomerDynamicObject : DynamicObject 
    {
        Dictionary<string, object> ViewData = new Dictionary<string, object>();

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            this.ViewData[binder.Name] = value;
            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = this.ViewData[binder.Name];
            return true;
        }
    }
}
