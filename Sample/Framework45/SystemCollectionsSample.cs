using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Framework45
{
    class SystemCollectionsSample:Executor
    {
        public SystemCollectionsSample() 
        {
            methodHandler += Test_RandArray_Sample;
        }

        public void Test_RandArray_Sample() 
        {
            Console.WriteLine("Generate rand numbers from 1 to 100 sample");
            Console.Write(string.Join(",", RandArray(1, 100)) + "\n");
        }

        /// <summary>
        /// 随机生成数组
        /// </summary>
        /// <param name="start"></param>
        /// <param name="conut"></param>
        /// <returns></returns>
        public int[] RandArray(int start, int conut) 
        {
            return Enumerable.Range(start, conut).OrderBy(m => Guid.NewGuid()).ToArray();
        }
    }
}
