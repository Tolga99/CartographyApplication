using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCartographyObjects
{
    interface IIsPointClose
    {
        int IsPointClose(double longitute, double latitute, double precision);
    }
}

