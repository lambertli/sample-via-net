using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Framework45
{
    class SystemLinqQueryabledSample
    {
        [TestMethod]
        public void Test_Query_1() 
        {
            string[] names = { "aaa", "bbb", "ccc" };
        }
    }

    class QueryProvider : IQueryProvider 
    {
        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable CreateQuery(System.Linq.Expressions.Expression expression)
        {
            throw new NotImplementedException();
        }

        public TResult Execute<TResult>(System.Linq.Expressions.Expression expression)
        {
            throw new NotImplementedException();
        }

        public object Execute(System.Linq.Expressions.Expression expression)
        {
            throw new NotImplementedException();
        }
    }
}
