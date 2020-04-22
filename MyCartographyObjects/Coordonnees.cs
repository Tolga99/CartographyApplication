using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCartographyObjects
{
	[Serializable]
	public class Coordonnees : CartoObj
	{
		private double _latitude;
		private double _longitude;
		#region Constructeurs
		public Coordonnees()
		{
			Latitude = 0;
			Longitude = 0;
		}

		public Coordonnees(double x, double y) : this()
		{
			Latitude = x;
			Longitude = y;
		}
		#endregion
		#region GET/SET
		public double Latitude
		{
			get { return _latitude; }
			set { _latitude = value; }
		}
		public double Longitude
		{
			get { return _longitude; }
			set { _longitude = value; }
		}
		#endregion
		#region Fonctions
		public void Affiche()
		{
			Console.WriteLine(ToString());
		}
		public override string ToString()
		{
			return "Id: " + base.Id + "  " + "Latitude: " + this.Latitude.ToString("F2") + "  " + "Longitude: " + this.Longitude.ToString("F2");
		}
		public bool IsEqual(Coordonnees objCoordonees)
		{
			if (objCoordonees.Id == this.Id)
				return true;
			else return false;
		}
		public int IsPointClose(double lati, double longi, double precision)
		{
			double x1, y1, distance;

			x1 = this.Latitude;
			y1 = this.Longitude;

			distance = MathUtil.Distance2Points(lati, longi, x1, y1);
			if (distance <= precision)
			{
				Console.WriteLine("Le point est proche !");
				return 1;
			}
			Console.WriteLine("Le point est trop eloigné !");
			return -1;
		}
		public int CompareTo(object c)
		{
			Coordonnees c2 = c as Coordonnees;

			if (this.Longitude < c2.Longitude)                  // x <
				return -1;

			if (this.Longitude == c2.Longitude)                 // x =
				if (this.Latitude < c2.Latitude)                  // y <
					return -1;
				else
					if (this.Latitude == c2.Latitude)             // y =
					return 0;
				else
					if (this.Latitude > c2.Latitude)              // y >
					return 1;

			if (this.Longitude > c2.Longitude)                  // x >
				return 1;

			return -2;
		}

		public int CompareTo(int c)
		{
			if (this.Longitude < c)                  // x <
				return -1;

			if (this.Longitude == c)                 // x =
				if (this.Latitude < c)                  // y <
					return -1;
				else
					if (this.Latitude == c)             // y =
					return 0;
				else
					if (this.Latitude > c)              // y >
					return 1;

			if (this.Latitude > c)                  // x >
				return 1;

			return -2;
		}
		#endregion
		#region Operator 
		public static bool operator <(Coordonnees c1, Coordonnees c2)
		{
			return c1.CompareTo(c2) < 0;
		}

		public static bool operator >(Coordonnees c1, Coordonnees c2)
		{
			return c1.CompareTo(c2) > 0;
		}

		public static bool operator <(Coordonnees c1, int c2)
		{
			return c1.CompareTo(c2) < 0;
		}

		public static bool operator >(Coordonnees c1, int c2)
		{
			return c1.CompareTo(c2) > 0;
		}

		public static Coordonnees operator *(Coordonnees c1, Coordonnees c2)
		{
			Coordonnees cT = new Coordonnees(c1.Longitude * c2.Longitude, c1.Latitude * c2.Latitude);

			return cT;
		}
		#endregion
	}
}
