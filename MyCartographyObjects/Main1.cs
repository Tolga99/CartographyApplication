using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.IO;

namespace MyCartographyObjects
{
    class Main1
    {
        static void Main(string[] args)
        {
            #region Phase 1
            //CREATION 2 OBJETS DE CHAQUE SORTE -----
            #region POLYLINE
            //Polyline po1 = new Polyline(Colors.Yellow, 2);
            //Polyline po2 = new Polyline(Colors.Gold, 3);
            ////po1.Draw();
            ////po2.Draw();
            #endregion

            #region POLYGONE
            //Polygone poly1 = new Polygone(Colors.Green, Colors.Gold, 1);
            //Polygone poly2 = new Polygone(Colors.Red, Colors.Gold, 0);
            ////poly1.Draw();
            ////poly2.Draw();
            #endregion

            #region COORDONNEES
            //Coordonnees Cd1 = new Coordonnees();
            //Coordonnees Cd2 = new Coordonnees(50, 100);
            ////Cd1.Affiche();
            ////Cd2.Affiche();
            #endregion

            #region POI
            //POI poi1 = new POI();
            //POI poi2 = new POI("Liege", 4420, 4000);
            ////poi1.Affiche();
            ////poi2.Affiche();
            #endregion

            #region P2 P3 P4 P5
            //List<CartoObj> Carte = new List<CartoObj>()
            //{
            //    poly1,
            //    poly2,
            //    po1,
            //    po2,
            //    Cd1,
            //    Cd2,
            //    poi1,
            //    poi2,
            //};

            //foreach (CartoObj ca in Carte)
            //{
            //    Console.WriteLine(ca);
            //}

            //foreach (CartoObj ca in Carte)
            //{
            //    if (ca is IPointy)
            //        Console.WriteLine(ca);
            //}

            // Console.WriteLine(sp);
            //foreach (CartoObj ca in Carte)
            //{
            //    if (!(ca is IPointy))
            //        Console.WriteLine(ca);
            //}
            #endregion
            //Ajouter ces objets dans une liste générique d’objets de type CartoObj (List<CartoObj>) ---------- 
            //Afficher cette liste en utilisant le mot clé foreach.
            //Afficher la liste des objets n’implémentant pas l’interface IPointy


            //Polyline pol2 = new Polyline();
            //pol2.Draw();

            //Polygone gone = new Polygone();
            //gone.Draw();


            // Polyline pl1 = new Polyline();
            // Polyline pl2 = new Polyline();
            // Polyline pl3 = new Polyline();
            // Polyline pl4 = new Polyline();
            // Polyline pl5 = new Polyline();

            // pl1.coord.Add(new Coordonnees() { Latitude = 1, Longitude = 0 });
            // pl1.coord.Add(new Coordonnees() { Latitude = 4, Longitude = 2 });

            // pl2.coord.Add(new Coordonnees() { Latitude = 2, Longitude = 0 });
            // pl2.coord.Add(new Coordonnees() { Latitude = 4, Longitude = 2 });

            // pl3.coord.Add(new Coordonnees() { Latitude = 20, Longitude = -15 });
            // pl3.coord.Add(new Coordonnees() { Latitude = 5, Longitude = 5 });
            // pl3.coord.Add(new Coordonnees() { Latitude = 7, Longitude = 8 });
            // pl3.coord.Add(new Coordonnees() { Latitude = 7, Longitude = 8 });

            // pl4.coord.Add(new Coordonnees() { Latitude = 6, Longitude = -3 });

            // pl5.coord.Add(new Coordonnees() { Latitude = 12, Longitude = 8 });

            // List<Polyline> line = new List<Polyline>()
            // {
            //     pl1,pl2,pl3,pl4,pl5,

            //};
            // line.Sort();//6.
            // foreach (Polyline data in line)
            // {
            //     data.Draw();
            // }//6.

            // MyPolylineBoundingBoxComparer bounding = new MyPolylineBoundingBoxComparer();  //7.
            // line.Sort(bounding); //7.
            // foreach (Polyline data in line)
            // {
            //     data.Draw();
            // }

            // Console.WriteLine("Polyline cherchee (pol4) : " + pl4);
            // Console.WriteLine("\nTrouvee : " + line.Find(x => x.Id == pl4.Id));

            // pl1.IsPointClose(1, 1, 2); // 9.


            // pl1.Equals(pl3);
            // Console.ReadKey();

            #endregion

            //byte[] array = new byte[10];
            //FileInfo f2 = new FileInfo(@"Test.dat");
            //FileStream fs = f2.Open(FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
            //if(f2.Exists)
            //     Console.WriteLine("Ouverture OK");
            //else
            //    Console.WriteLine("Existe Pas");
            //array[0] = 0;
            //array[1] = 1;
            //fs.Write(array, 0,2);
            //fs.Close();
            MyPersonalMapData data = new MyPersonalMapData();
            Console.WriteLine(data);
            //data.SaveAsBinary(data, @"C:\Users\t_olg\Documents\monfichier.dat");
           // var madata = data.LoadAsBinary(@"C:\Users\t_olg\Documents\monfichier.dat");
            Console.ReadKey();











        }
    }
}
