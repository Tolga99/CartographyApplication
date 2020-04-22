using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MyCartographyObjects
{
    [Serializable]
    public class Polygone : CartoObj, IIsPointClose, IPointy,ICartoObj
    {
        public List<Coordonnees> coord;
        private double _opacite;

        [NonSerialized]
        private Color _remplissage;
        public Color Remplissage
        {
            get => _remplissage;
            set
            {
                _remplissage = value;
                _colRemplissageString = _remplissage.ToString();
            }
        }

        private string _colRemplissageString;
        public string ColRemplissageString
        {
            get => _colRemplissageString;
            set => _colRemplissageString = value;
        }


        [NonSerialized]
        private Color _contour;
        public Color Contour
        {
            get => _contour;
            set
            {
                _contour = value;
                _colContourString = _contour.ToString();
            }
        }

        private string _colContourString;
        public string ColContourString
        {
            get => _colContourString;
            set => _colContourString = value;
        }

        private int nbpoints;
        private string _name;
        public Polygone() : base()
        {
            coord = new List<Coordonnees>();
            Contour = Colors.Green;
            Opacite = 0.2;
            Remplissage = Colors.Red;
            Name = "Default Name";
        }
        public Polygone(Color contor, Color remplis) : base()
        {
            coord = new List<Coordonnees>();
            Contour = contor;
            Opacite = 0.2;
            Remplissage = remplis;
            Name = "Default Name";
        }
        public Polygone(List<Coordonnees> Cor) : base()
        {
            coord = Cor;
            Contour = Colors.Green;
            Opacite = 0.2;
            Remplissage = Colors.Red;
            Name = "Default Name";
        }
        public Polygone(List<Coordonnees> Cor, Color contor, Color remplis) : base()
        {
            coord = Cor;
            Contour = contor;
            Opacite = 0.2;
            Remplissage = remplis;
            Name = "Default Name";
        }
        public Polygone(List<Coordonnees> Cor, Color rempli, Color cont, int opacite, string name) : base()
        {
            coord = Cor;
            Remplissage = rempli;
            Contour = cont;
            Opacite = opacite;
            Name = name;
        }
        public Polygone(List<Coordonnees> Cor, int opacite,string name) : base()
        {
            coord = Cor;
            Opacite = opacite;
            Name = name;
        }
        public double Opacite
        {
            get { return _opacite; }
            set { _opacite = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public void AddCoord(double x, double y)
        {
            Coordonnees c = new Coordonnees(x, y);
            coord.Add(c);
        }

        public void AddCoord(Coordonnees c)
        {
            coord.Add(c);
        }
        public int NbPoints
        {
            get
            {
                return coord.Select(a => a.Id).Distinct().Count();
            }
        }
        public int GetNbPoints()
        {
            nbpoints = coord.Count;
            return coord.Count;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Polyline :");
            foreach (Coordonnees data in coord)
            {
                sb.AppendLine("Collection :" + data);
            }
           // sb.AppendLine("Opacite :" + this.Opacite + " " + "Remplissage :" + this.Remplissage + " " + "Contour :" + this.Contour);
            return sb.ToString();
        }
        public List<Coordonnees> GetCoordonnees()
        {
            nbpoints = coord.Count;
            return coord;
        }
        public override void Draw()
        {
            Console.WriteLine(ToString());
        }

        public int IsPointClose(double lati, double longi, double precision)
        {
            double xMAX = 0, yMAX = 0, xMIN, yMIN;
            foreach (Coordonnees data in coord)
            {
                if (xMAX < data.Latitude)
                    xMAX = data.Latitude;
                if (yMAX < data.Longitude)
                    yMAX = data.Longitude;
            }
            xMIN = xMAX;
            yMIN = yMAX;
            foreach (Coordonnees data in coord)
            {
                if (xMIN > data.Latitude)
                    xMIN = data.Latitude;
                if (yMIN > data.Longitude)
                    yMIN = data.Longitude;
            }
            if (lati <= xMAX && lati >= xMIN && longi <= yMAX && longi >= yMIN)
            {
                Console.WriteLine("Point dans la bounding box");
                return 1;
            }
            return -1;
        }
    }
}
