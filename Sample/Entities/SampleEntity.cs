using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Entities
{
    public class SampleEntity
    {
        public SampleEntity()
        {
            this.CreatedAt = DateTime.Now;
        }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}
