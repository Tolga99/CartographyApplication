using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCartographyObjects
{
    class MyPolylineBoundingBoxComparer : IComparer<Polyline>
    {
        public int Compare(Polyline x, Polyline y)
        {
            double xMAX = 0, yMAX = 0, xMIN, yMIN;
            foreach (Coordonnees data in x.coord)
            {
                if (xMAX < data.Latitude)
                    xMAX = data.Latitude;
                if (yMAX < data.Longitude)
                    yMAX = data.Longitude;
            }
            xMIN = xMAX;
            yMIN = yMAX;
            foreach (Coordonnees data in x.coord)
            {
                if (xMIN > data.Latitude) xMIN = data.Latitude;
                if (yMIN > data.Longitude) yMIN = data.Longitude;
            }

            double xMAX2 = 0, yMAX2 = 0, xMIN2, yMIN2;
            foreach (Coordonnees data in y.coord)
            {
                if (xMAX2 < data.Latitude)
                    xMAX2 = data.Latitude;
                if (yMAX2 < data.Longitude)
                    yMAX2 = data.Longitude;
            }
            xMIN2 = xMAX2;
            yMIN2 = yMAX2;
            foreach (Coordonnees data in y.coord)
            {
                if (xMIN2 > data.Latitude)
                    xMIN2 = data.Latitude;
                if (yMIN2 > data.Longitude)
                    yMIN2 = data.Longitude;
            }
            double long1 = MathUtil.Distance2Points(xMAX, yMAX, xMAX, yMIN);
            double larg1 = MathUtil.Distance2Points(xMAX, yMAX, xMIN, yMAX);

            double long2 = MathUtil.Distance2Points(xMAX2, yMAX2, xMAX2, yMIN2);
            double larg2 = MathUtil.Distance2Points(xMAX2, yMAX2, xMIN2, yMAX2);

            if ((long1 * larg1) < (long2 * larg2)) // x passe en premier dans l'ordre croissant( c le petit)
                return -1;
            if ((long1 * larg1) == (long2 * larg2)) // x passe en premier dans l'ordre croissant( c le petit)
                return 0;
            else return 1;
        }
    }
}
