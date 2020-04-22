using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace MyCartographyObjects
{
    [Serializable]
    public class Polyline : CartoObj, IPointy, IComparable<Polyline>, IEquatable<Polyline>, ICartoObj
    {
        public List<Coordonnees> coord;
        private int _epaisseur;
        private int nbPoints;
        private string _name;

        [NonSerialized]
        private Color _colrs;
        public Color Colrs
        {
            get => _colrs;
            set
            {
                _colrs = value;
                _colString = _colrs.ToString();
            }
        }

        private string _colString;
        public string ColString
        {
            get => _colString;
            set => _colString = value;
        }

        public Polyline() : base()
        {
            coord = new List<Coordonnees>();
            Epaisseur = 1;
            Colrs = Colors.Blue;
            Name = "Default Name";
        }
        public Polyline(Color col) : base()
        {
            coord = new List<Coordonnees>();
            Epaisseur = 1;
            Colrs = col;
            Name = "Default Name";
        }
        public Polyline(List<Coordonnees> cord) : base()
        {
            coord = cord;
            Epaisseur = 1;
            Colrs = Colors.Blue;
            Name = "Default Name";
        }
        public Polyline(List<Coordonnees> cord, Color couleur) : base()
        {
            coord = cord;
            Epaisseur = 1;
            Colrs = couleur;
            Name = "Default Name";
        }
        public Polyline(List<Coordonnees> cord, Color couleur, int epaisseur) : base()
        {
            coord = cord;
            Epaisseur = epaisseur;
            Colrs = couleur;
            Name = "Default Name";
        }
        public Polyline(List<Coordonnees> cord, Color couleur, int epaisseur, string name) : base()
        {
            coord = cord;
            Epaisseur = epaisseur;
            Colrs = couleur;
            Name = name;
        }

        public List<Coordonnees> GetCoordonnees()
        {
            return coord;
        }
        public int GetNbPoints()
        {
            nbPoints = coord.Count;
            return coord.Count;
        }
        public int Epaisseur
        {
            get { return _epaisseur; }
            set { _epaisseur = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        public int NbPoints
        {
            get
            {
                return coord.Select(a => a.Id).Distinct().Count();
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Polyline :");
            foreach (Coordonnees data in coord)
            {
                sb.AppendLine("Collection :" + data);
            }
            //sb.AppendLine(" Couleur:" + this.Colrs + " " + "Epaisseur: " + this.Epaisseur);
            sb.AppendLine("Nombre de Points: " + this.NbPoints.ToString());

            return sb.ToString();
        }

        public override void Draw()
        {
            Console.WriteLine(ToString());
        }
        public int IsPointClose(double lati, double longi, double precision)
        {
            double x1, y1, x2 = 0, y2 = 0, distance;
            for (int i = 0; i < coord.Count; i++)
            {

                x1 = coord[i].Latitude;
                y1 = coord[i].Longitude;
                if (i + 1 > coord.Count)
                {
                    Console.WriteLine("Le point est trop eloigné !");
                    return -1;
                }
                x2 = coord[i + 1].Latitude;
                y2 = coord[i + 1].Longitude;


                distance = MathUtil.DistanceSegPoint(lati, longi, x1, y1, x2, y2);
                if (distance <= precision)
                {
                    Console.WriteLine("Le point est proche !");
                    return 1;
                }
            }
            Console.WriteLine("Le point est trop eloigné !");
            return -1;
        }

        public int CompareTo(Polyline a)
        {

            int i;
            double x1, y1, x2 = 0, y2 = 0, distance1 = 0, distance2 = 0;
            for (i = 0; i < coord.Count; i++)
            {

                x1 = coord[i].Latitude;
                y1 = coord[i].Longitude;
                if (i + 1 < coord.Count)
                {

                    x2 = coord[i + 1].Latitude;
                    y2 = coord[i + 1].Longitude;


                    distance1 += MathUtil.Distance2Points(x1, y1, x2, y2);
                }

            }
            for (i = 0; i < a.coord.Count; i++)
            {

                x1 = a.coord[i].Latitude;
                y1 = a.coord[i].Longitude;
                if (i + 1 < a.coord.Count)
                {

                    x2 = a.coord[i + 1].Latitude;
                    y2 = a.coord[i + 1].Longitude;


                    distance2 += MathUtil.Distance2Points(x1, y1, x2, y2);
                }
            }
            if (distance1 < distance2) //DISTANCE 1 PASSE EN PREMIER DANS L'ORDRE CROISSANT
                return -1;
            if (distance1 == distance2)
                return 0;
            else return 1;
        }

        public bool Equals(Polyline a)
        {
            double x1, y1, x2 = 0, y2 = 0, distance1 = 0, distance2 = 0;
            for (int i = 0; i < coord.Count; i++)
            {

                x1 = coord[i].Latitude;
                y1 = coord[i].Longitude;
                if (i + 1 < coord.Count)
                {

                    x2 = coord[i + 1].Latitude;
                    y2 = coord[i + 1].Longitude;


                    distance1 += MathUtil.Distance2Points(x1, y1, x2, y2);
                }

            }
            for (int i = 0; i < a.coord.Count; i++)
            {

                x1 = a.coord[i].Latitude;
                y1 = a.coord[i].Longitude;
                if (i + 1 < a.coord.Count)
                {

                    x2 = a.coord[i + 1].Latitude;
                    y2 = a.coord[i + 1].Longitude;


                    distance2 += MathUtil.Distance2Points(x1, y1, x2, y2);
                }
            }
            if (distance1 == distance2) //DISTANCE 1 PASSE EN PREMIER DANS L'ORDRE CROISSANT
            {
                return true;
            }
            else return false;
        }

        public static bool operator <(Polyline c1, Polyline c2)
        {
            return c1.CompareTo(c2) < 0;
        }

        public static bool operator >(Polyline c1, Polyline c2)
        {
            return c1.CompareTo(c2) > 0;
        }

        public static bool operator ==(Polyline c1, Polyline c2)
        {
            return c1.CompareTo(c2) == 0;
        }

        public static bool operator !=(Polyline c1, Polyline c2)
        {
            return c1.CompareTo(c2) != 0;
        }

        //public bool Equals(Polyline c2)
        //{
        //    if (c2 == null) return false;
        //    return this.CompareTo(c2) == 0;
        //}

    }
}
