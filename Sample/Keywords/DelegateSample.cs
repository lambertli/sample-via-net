using Sample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Keywords
{
    class DelegateSample : Executor
    {
        Cat _cat = new Cat("Tom");
        Mouse[] _mouses = new Mouse[7] 
        {
            new Mouse("Jack"),
            new Mouse("Aaron"),
            new Mouse("Clarence"),
            new Mouse("Daniel"),
            new Mouse("Ellis"),
            new Mouse("Gary"),
            new Mouse("Gordon")
        };

        public DelegateSample() 
        {
            methodHandler += Test_Sample_1;
            methodHandler += Test_Sample_2;
        }

        /// <summary>
        /// Delegate method
        /// </summary>
        public void Test_Sample_1() 
        {
            Console.WriteLine("Sample cat scream and mouse run awawy");
            foreach (var mouse in _mouses) 
            {
                _cat.ScreamHandler += mouse.Run;
            }

            _cat.Scream();
        }

        /// <summary>
        /// Action method
        /// </summary>
        public void Test_Sample_2()
        {
            Console.WriteLine("Sample cat scream and mouse run awawy");
            IList<Action> actions = new List<Action>();
            foreach (var mouse in _mouses)
            {
                actions.Add(() => { mouse.Run(_cat); });
            }

            _cat.Scream();
        }
    }
}
