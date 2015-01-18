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

        /// <summary>
        /// Delegate method
        /// </summary>
        [TestMethod]
        public void Test_KW_Delegate() 
        {
            Console.WriteLine("Sample cat scream and mouse run away");
            foreach (var mouse in _mouses) 
            {
                _cat.ScreamHandler += mouse.RunAway;
            }
            _cat.Scream();
        }

        /// <summary>
        /// Action method
        /// </summary>
        [TestMethod]
        public void Test_KW_Action()
        {
            Console.WriteLine("Sample cat scream and mouse run away");
            IList<Action> actions = new List<Action>();
            foreach (var mouse in _mouses)
            {
                actions.Add(() => { mouse.RunAway(_cat); });
            }

            _cat.Scream();
        }
    }
}
