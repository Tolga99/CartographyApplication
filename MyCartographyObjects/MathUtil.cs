using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCartographyObjects
{
    static class MathUtil
    {
        public static double Distance2Points(double x1, double y1, double x2, double y2)
        {
            double x, y, distance;
            x = x2 - x1;
            y = y2 - y1;
            x = Math.Pow(x, 2);
            y = Math.Pow(y, 2);
            distance = x + y;
            return Math.Sqrt(distance);
            //return distance;
        }
        public static double DistanceSegPoint(double x, double y, double x1, double y1, double x2, double y2)
        {
            //x et y point 
            // x1 x2 y1 y2 segment
            double A = x - x1;
            double B = y - y1;
            double C = x2 - x1;
            double D = y2 - y1;

            double dot = A * C + B * D;
            double len_sq = C * C + D * D;
            double param = -1;
            if (len_sq != 0) //in case of 0 length line
                param = dot / len_sq;

            double xx, yy;

            if (param < 0)
            {
                xx = x1;
                yy = y1;
            }
            else if (param > 1)
            {
                xx = x2;
                yy = y2;
            }
            else
            {
                xx = x1 + param * C;
                yy = y1 + param * D;
            }

            var dx = x - xx;
            var dy = y - yy;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
