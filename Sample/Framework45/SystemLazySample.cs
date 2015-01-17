using Sample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Framework45
{
    class SystemLazySample : Executor
    {
        [TestMethod]
        public void Test_Normal_Create()
        {
            Console.WriteLine("Start get larger object normal...");

            IList<LargeObject> list = this.GetLargeObjects();
            Console.WriteLine("Total object in my list: {0}", list.Count);
            foreach (var obj in list)
            {
                Console.WriteLine("Id:{0} Title:{1}", obj.Id, obj.Title);
            }

            Console.WriteLine("Completed!");
        }

        [TestMethod]
        public void Test_Lazy_Create()
        {
            Console.WriteLine("Start get larger object lazy...");

            IList<Lazy<LargeObject>> list = this.GetLargeObjectsLazy();
            Console.WriteLine("Total object in my list: {0}", list.Count);
            foreach (var obj in list)
            {
                Console.WriteLine("Id:{0} Title:{1}", obj.Value.Id, obj.Value.Title);
            }

            Console.WriteLine("Completed!");
        }

        public IList<LargeObject> GetLargeObjects()
        {
            return new List<LargeObject> 
            {
                new LargeObject{ Id=1, Title="LargeObject 1" },
                new LargeObject{ Id=2, Title="LargeObject 2" },
                new LargeObject{ Id=3, Title="LargeObject 3" }
            };
        }

        public IList<Lazy<LargeObject>> GetLargeObjectsLazy()
        {
            return new List<Lazy<LargeObject>> 
            {
                new Lazy<LargeObject>(()=>{ return new LargeObject(){ Id=1, Title="LargeObject 1" }; }),
                new Lazy<LargeObject>(()=>{ return new LargeObject(){ Id=2, Title="LargeObject 2" }; }),
                new Lazy<LargeObject>(()=>{ return new LargeObject(){ Id=3, Title="LargeObject 3" }; })
            };
        }
    }
}
