using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Entities
{
    public class LargeObject : SampleEntity
    {
        public LargeObject() 
        {
            Thread.Sleep(3000);
        }

        public int Id { get; set; }
        public string Title { get; set; }
    }
}
