using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Media;

namespace MyCartographyObjects
{
    [Serializable]
    public class MyPersonalMapData : CartoObj
    {
        private string _nom;
        private string _prenom;
        private string _email;

        public List<ICartoObj> ObservableCollection;

        #region Constructeurs
        public MyPersonalMapData()
        {
            Nom = "Ovali";
            Prenom = "Tolga";
            Email = "Tolga.Ovali@hepl.student.be";
            ObservableCollection = new List<ICartoObj>();
        }

        public MyPersonalMapData(string nom, string prenom, string email) : this()
        {
            Nom = nom;
            Prenom = prenom;
            Email = email;
        }
        #endregion
        #region GET/SET
        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }
        public string Prenom
        {
            get { return _prenom; }
            set { _prenom = value; }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        #endregion

        public void Affiche()
        {
            Console.WriteLine(ToString());
        }
        public override string ToString()
        {
            return "Id: " + base.Id + "  " + " Nom: " + this.Nom + "  " + "Prenom: " + this.Prenom + "  " + "Email: " + this.Email;
        }
        public void SaveAsBinary(string dossierTravail = "")
        {
            BinaryFormatter binFormat = new BinaryFormatter();
            string path = Path.Combine(dossierTravail, $"{Nom}{Prenom}.dat");
            using (Stream fStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binFormat.Serialize(fStream, this);
            }
        }
        public MyPersonalMapData LoadAsBinary(string path = "")
        {
            if (path == "")
                path = $"{Nom}{Prenom}.dat";

            BinaryFormatter binFormat = new BinaryFormatter();
            using (Stream fstream = File.OpenRead(path))
            {
                MyPersonalMapData myPersonalMapData = (MyPersonalMapData)binFormat.Deserialize(fstream);
                if (myPersonalMapData.ObservableCollection.Count > 0)
                {
                    CartoObj.id_generator = myPersonalMapData.ObservableCollection.Cast<CartoObj>().Max(a => a.Id);

                    foreach(var col in myPersonalMapData.ObservableCollection)
                    {
                        if(col is Polyline)
                        {
                            var pol = col as Polyline;
                            pol.Colrs = (Color)ColorConverter.ConvertFromString(pol.ColString);
                        }
                        if(col is Polygone)
                        {
                            var pog = col as Polygone;
                            pog.Contour = (Color)ColorConverter.ConvertFromString(pog.ColContourString);
                            pog.Remplissage = (Color)ColorConverter.ConvertFromString(pog.ColRemplissageString);
                        }
                    }
                }


                return myPersonalMapData;
            }
        }
        public bool BinaryExist()
        {
            return File.Exists(Nom + Prenom + ".dat");
        }
        public void ClearCollection()
        {
            ObservableCollection.Clear();
        }

    }
}