using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCartographyObjects 
{
    [Serializable]
    public class POI : Coordonnees, IIsPointClose
    {
        private string _description;

        public POI() : base(50.61000, 5.510000)
        {
            Description = "HEPL";
        }

        public POI(string name, double x, double y)
        {
            Description = name;
            base.Latitude = x;
            base.Longitude = y;
        }
        public Coordonnees GetCoordonnees()
        {
            return new Coordonnees(Longitude, Latitude);
        }
        public void Affiche()
        {
            Console.WriteLine(ToString());
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public override string ToString()
        {
            return base.ToString() + " Description: " + Description;
        }

        public int IsPointClose(double lati, double longi, double precision)
        {
            double x1, y1, distance;

            x1 = base.Latitude;
            y1 = base.Longitude;

            distance = MathUtil.Distance2Points(lati, longi, x1, y1);
            if (distance <= precision)
            {
                Console.WriteLine("Le point est proche !");
                return 1;
            }
            Console.WriteLine("Le point est trop eloigné !");
            return -1;
        }
    }
}
