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
        [TestMethod]
        public void Test_GetField_Infos() 
        {
            MyClass target = new MyClass();
            Type type = target.GetType();
            FieldInfo[] finfos = type.GetFields();
            foreach (var info in finfos)
            {
                Console.WriteLine("name:{0}\t type:{1}", info.Name, info.FieldType);
            }
        }

        [TestMethod]
        public void Test_GetMethod_Infos() 
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

        [TestMethod]
        public void Test_Create_Instance() 
        {
            Assembly assembly = this.GetType().Assembly;

            Console.WriteLine("Create instance with non-parameters constructor.");
            SampleEntity entity1 = assembly.CreateInstance("Sample.Entities.SampleEntity") as SampleEntity;
            Console.WriteLine("Create by Assembly.CreateInstance {0}", entity1 == null ? "null" : entity1.ToString());
            
            SampleEntity entity2 = Activator.CreateInstance(typeof(SampleEntity)) as SampleEntity;
            Console.WriteLine("Create by Activator.CreateInstance {0}", entity1 == null ? "null" : entity2.ToString());
            
            SampleEntity entity3 = Activator.CreateInstance<SampleEntity>();
            Console.WriteLine("Create by Activator.CreateInstance<T> {0}", entity1 == null ? "null" : entity3.ToString());

            Console.WriteLine("Create instance with parameters constructor.");
            Cat tom = assembly.CreateInstance("Sample.Entities.Cat", true, BindingFlags.CreateInstance, null, new object[] { "Tom" }, null, null) as Cat;
            Console.WriteLine("Create by Assembly.CreateInstance {0}", tom == null ? "null" : tom.ToString());

            Cat mat = (Cat)Activator.CreateInstance(typeof(Cat),"Tome");
            Console.WriteLine("Create by Activator.CreateInstance {0}", mat == null ? "null" : mat.ToString());

            Console.WriteLine("Create instance with default<T>.");
            Cat lee = default(Cat);
            Console.WriteLine("Create by default {0}", lee == null ? "null" : lee.ToString());

            DateTime dt = default(DateTime);
            Console.WriteLine("Create datetime by default {0}", dt == null ? "null" : dt.ToString());
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
