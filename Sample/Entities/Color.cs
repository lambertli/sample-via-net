using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Entities
{
    [Flags]
    enum Color
    {
        While = 0x0001,
        Red = 0x0002,
        Green = 0x0004,
        Blue = 0x0008,
        Orange = 0x0010
    }
}
