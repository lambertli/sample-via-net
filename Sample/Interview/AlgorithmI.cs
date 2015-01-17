using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Interview
{
    class AlgorithmI : Executor
    {
        /// <summary>
        /// 找出一个数组中重复次数大于等于minTimes的数字。
        /// 例如：[1,1,1,2,2,5,2,4,9,9,20]
        /// 中重复次数大于等于2的数字为：1、2、9
        /// 接口声明为：
        ///     interface IDuplicationFinder
        ///     {
        ///         int[] FindDuplication(int[] input, uint minTimes);
        ///     }
        /// </summary>
        [TestMethod]
        public void Test_Sample_1()
        {
            int[] input = { 1, 1, 1, 2, 2, 5, 2, 4, 9, 9, 20 };
            uint minTimes = 2;
            int[] result = new DuplicationFinder().FindDuplication(input, minTimes);

            Console.WriteLine("input:[{0}], minTimes:{1}", string.Join(",", input), minTimes);
            Console.WriteLine("output:" + string.Join(",", result));
        }


        /// <summary>
        /// 将输入中文字符串，以逗号和句号为界，分段颠倒
        /// 如：
        ///     白日依山尽，黄河入海流。欲穷千里目，更上一层楼。
        /// 输出：
        ///     尽山依日白，流海入河黄。目里千穷欲，楼层一上更。
        /// 接口声明为：
        ///     interface IStringInverter
        ///     {
        ///         string PiecewiseInvert(string input);
        ///     }
        /// </summary>
        [TestMethod]
        public void Test_Sample_2() 
        {
            string input = "白日依山尽，黄河入海流。欲穷千里目，更上一层楼。";
            string result = new StringInverter().PiecewiseInvert(input);

            Console.WriteLine("input:{0}", input);
            Console.WriteLine("output:{0}", result);
        }
    }

    interface IDuplicationFinder 
    {
        int[] FindDuplication(int[] input, uint minTimes);
    }
    public class DuplicationFinder : IDuplicationFinder
    {
        public int[] FindDuplication(int[] input, uint minTimes)
        {
            return input.GroupBy(m => m).Where(g=>g.Count() >= minTimes).Select(g=>g.Key).ToArray();
        }
    }

    interface IStringInverter 
    {
        string PiecewiseInvert(string input);
    }
    public class StringInverter : IStringInverter
    {
        public string PiecewiseInvert(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            char[] splitSymbles = new char[] { ',', '.', '，', '。' };
            char[] segmentSymbles = input.Where(m => splitSymbles.Contains(m)).Concat(new char[] { ' ' }).ToArray();
            return string.Join("", input.Split(splitSymbles).Select((m, i) => new String(m.Reverse().ToArray()) + segmentSymbles[i]).ToArray()).TrimEnd(' ');
        }
    }

}
