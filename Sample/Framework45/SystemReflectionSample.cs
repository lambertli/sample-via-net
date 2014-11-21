using Sample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Framework45
{
    class SystemReflectionSample : Executor
    {

        public SystemReflectionSample() 
        {
            methodHandler += Test_Field_Sample;
            methodHandler += Test_Method_Sample;
        }

        public void Test_Field_Sample() 
        {
            MyClass target = new MyClass();
            Type type = target.GetType();
            FieldInfo[] finfos = type.GetFields();
            foreach (var info in finfos)
            {
                Console.WriteLine("name:{0}\t type:{1}", info.Name, info.FieldType);
            }
        }

        public void Test_Method_Sample() 
        {
            MyClass target = new MyClass();
            Type type = target.GetType();

            Console.WriteLine("Get public methods");
            var defMethods = type.GetMethods();
            foreach (var method in defMethods) 
            {
                Console.WriteLine("name:{0}\t params:{1} return:{2}", method.Name, method.GetParameters().Count(), method.ReturnType);
            }

            Console.WriteLine("Get non public methods");
            var addMethods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Static);
            foreach (var method in addMethods)
            {
                Console.WriteLine("name:{0}\t params:{1} return:{2}", method.Name, method.GetParameters().Count(), method.ReturnType);
            }
        }

        class MyClass 
        {
            private int _id;
            protected int prid;
            internal int inid;
            public int puid;

            private string PrivateName { get; set; }
            protected string ProtectedName { get; set; }
            internal string InternalName { get; set; }
            public string PublicName { get; set; }

            public static string StaticPrintAndReturn(string msg) 
            {
                StaticPrint(msg);
                return msg;
            }

            public string PrintAndReturn(string msg) 
            {
                Print(msg);
                return msg;
            }


            private static void StaticPrint(string msg)
            {
                Console.WriteLine(msg);
            }

            private void Print(string msg) 
            {
                Console.WriteLine(msg);
            }
        }
    }
}
