using Microsoft.Maps.MapControl.WPF;
using Microsoft.VisualBasic.FileIO;
using MyCartographyObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
//using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
namespace PersonalMap_Manager
{

    public partial class Map : Window
    {
        string DossierTravail = "";
        public Color ListBoxBackgroundColor { get; set; } = Colors.White;
        public Color ListBoxTextColor { get; set; } = Colors.Black;

        double latitude;
        double longitude;
        MyPersonalMapData _myMapData;
        Option opW = new Option();
        public Color LineP = Colors.Black;
        public Color BackP = Colors.Red;

        public Map(MyPersonalMapData myMapData)
        {
            InitializeComponent();
            MapCore point = new MapCore();
            MapCore map = new MapCore();
            _myMapData = myMapData;
            listBox.SelectionMode = System.Windows.Controls.SelectionMode.Extended;
            listBox.ItemsSource = _myMapData.ObservableCollection;
            LoadCoordoonees();
            this.Title += " User: " + myMapData.Nom + " " + myMapData.Prenom + " " + myMapData.Email;
        }
        #region METHODES
        private void LoadCoordoonees()  //CHARGE LE CONTENU DU FICHIER S'IL EXISTE
        {
            foreach (var co in _myMapData.ObservableCollection)
            {
                if ((co as Coordonnees) is var point && point != null)
                {
                    Location loc = new Location(point.Latitude, point.Longitude);
                    Pushpin pin = new Pushpin();
                    pin.Location = loc;
                    pin.Tag = point.Id;
                    MyMap.Children.Add(pin);
                }
                if (co is MyCartographyObjects.Polyline)
                {
                    List<Coordonnees> c = new List<Coordonnees>();
                    var ac = co as MyCartographyObjects.Polyline;
                    MyCartographyObjects.Polyline line = new MyCartographyObjects.Polyline(ac.coord, LineP);
                    foreach (var pt in line.coord)
                    {
                        AddPushPin(pt);
                    }
                    DrawLine(line.coord, line.Id, line.Colrs);
                }
                if (co is Polygone)
                {
                    List<Coordonnees> c = new List<Coordonnees>();
                    var ac = co as Polygone;
                    Polygone gone = new Polygone(ac.coord, LineP, BackP);
                    foreach (var pt in gone.coord)
                    {
                        AddPushPin(pt);
                    }
                    DrawGone(gone.coord, gone.Id, gone.Contour, gone.Remplissage);
                }
            }
        }
        private void AddPushPin(Coordonnees point) //AJOUTE UN POINT SUR LA CARTE ET DANS LA LISTE AVEC LES COORDONNES RECUES
        {
            Pushpin pin = new Pushpin();
            Location loc = new Location(point.Latitude, point.Longitude);
            pin.Location = loc;
            pin.Tag = point.Id;
            MyMap.Children.Add(pin);
            //listBox.Items.Add(point);
        }
        private void MapDoubleClick(object sender, MouseButtonEventArgs e) //RECUPERE LES COORDONNES DE L'ENDROIT QUI A ETE CLICKER ET LES ENVOI A ADDPUSHPIN
        {
            e.Handled = true;
            Point p0 = e.GetPosition(MyMap);
            Location loc = MyMap.ViewportPointToLocation(p0);
            Coordonnees point = new Coordonnees(loc.Latitude, loc.Longitude);
            AddPushPin(point);
            _myMapData.ObservableCollection.Add(point);
            listBox.Items.Refresh();
        }
        private void DrawLine(List<Coordonnees> liste, int id, Color col) //DESSINE LES LIGNES ENTRES LES COORDONNEES DE LA POLYLINE 
        {
            LocationCollection loc = new LocationCollection();
            for (int i = 0; i < liste.Count; i++)
            {
                if (i + 1 < liste.Count)
                {
                    Location loc1 = new Location(liste[i].Latitude, liste[i].Longitude);
                    Location loc2 = new Location(liste[i + 1].Latitude, liste[i + 1].Longitude);
                    loc.Add(loc1);
                    loc.Add(loc2);
                }
            }
            MapPolyline line = new MapPolyline();
            line.Stroke = new SolidColorBrush(col);
            line.StrokeThickness = 3.0;
            line.Locations = loc;
            line.Tag = id.ToString();
            MyMap.Children.Add(line);
        }
        private void DrawGone(List<Coordonnees> liste, int id, Color strk, Color fill) //DESSINE LES LIGNES ET REMPLIS L'ESPACE ENTRES LES COORDONNEES DU POLYGONE 
        {

            LocationCollection loc = new LocationCollection();
            for (int i = 0; i < liste.Count; i++)
            {
                if (i + 1 < liste.Count)
                {
                    Location loc1 = new Location(liste[i].Latitude, liste[i].Longitude);
                    Location loc2 = new Location(liste[i + 1].Latitude, liste[i + 1].Longitude);
                    loc.Add(loc1);
                    loc.Add(loc2);
                }
            }
            Location loc3 = new Location(liste[0].Latitude, liste[0].Longitude);
            Location loc4 = new Location(liste[liste.Count - 1].Latitude, liste[liste.Count - 1].Longitude);
            loc.Add(loc3);
            loc.Add(loc4);
            MapPolygon gone = new MapPolygon();
            gone.Tag = id.ToString();
            gone.Stroke = new SolidColorBrush(strk);
            gone.StrokeThickness = 3.0;
            gone.Locations = loc;
            gone.Fill = new SolidColorBrush(fill) { Opacity = 0.3 };
            MyMap.Children.Add(gone);
        }
        #endregion
        #region BUTTONS
        private void Ajouter_Click(object sender, RoutedEventArgs e) // AJOUTE UN POI, MANUELLEMENT , LES COORDOONEES DOIVENT ETRE CONNUES PAR L'UTLISATEUR !! 
        {
            var mwDialog = new MainWindow();
            if (mwDialog.ShowDialog() == true)
            {
                Location loc = new Location(mwDialog.point.Latitude, mwDialog.point.Longitude);
                Pushpin pin = new Pushpin();
                pin.Location = loc;
                pin.Tag = mwDialog.point.Id;
                MyMap.Children.Add(pin);
                _myMapData.ObservableCollection.Add(mwDialog.point);
                listBox.Items.Refresh();
            }

        }

        private void Modifier_Click(object sender, RoutedEventArgs e) //Lors de la modification , il suffit de cliquer sur un element et d'ensuite de cliquer sur modifier. 
        {                                                             //Apres cela il suffit juste d'encoder une Description. La latitude et la longitude ne sont pas considerees...
                                                                      //MODIFIER NE FONCTIONNE QU'AVEC UN SEUL POINT !!!!! DONC NI DE POLYLINE NI DE POLYGONE  !!

            if (listBox.SelectedItem is Polygone || listBox.SelectedItem is MyCartographyObjects.Polyline)
            {
                return;
            }

            Coordonnees selectedPOI = (listBox.SelectedItem as Coordonnees);

            longitude = selectedPOI.Longitude;
            latitude = selectedPOI.Latitude;
            _myMapData.ObservableCollection.Remove(selectedPOI);

            var mwDialog = new MainWindow();
            if (mwDialog.ShowDialog() == true)
            {
                //Retour de la dialog, recharge le fichier
                POI point = new POI(mwDialog.TextBoxDescription.Text, latitude, longitude);
                Location loc = new Location(latitude, longitude);
                Pushpin pin = new Pushpin();
                pin.Location = loc;
                pin.Tag = point.Id;
                MyMap.Children.Add(pin);
                _myMapData.ObservableCollection.Add(point);
                listBox.Items.Refresh();

            }
        }

        private void Supprimer_Click(object sender, RoutedEventArgs e) //SUPPRIME UN SEUL POINT , UNE POLYLINE OU UN POLYGONE 
        {
            if ((listBox.SelectedItem as CartoObj) is var obj && obj != null)
            {
                _myMapData.ObservableCollection.Remove(obj);
                List<string> ListTotalPoint = new List<string>();

                if (obj is Polygone)
                {
                    var objPolygone = obj as Polygone;
                    ListTotalPoint.Add(objPolygone.Id.ToString());

                    foreach (var co in objPolygone.coord)
                        ListTotalPoint.Add(co.Id.ToString());
                }
                else if (obj is MyCartographyObjects.Polyline)
                {
                    var objPolyline = obj as MyCartographyObjects.Polyline;
                    ListTotalPoint.Add(objPolyline.Id.ToString());

                    foreach (var co in objPolyline.coord)
                        ListTotalPoint.Add(co.Id.ToString());
                }
                else if (obj is Coordonnees)
                {
                    var objCoor = obj as Coordonnees;
                    ListTotalPoint.Add(objCoor.Id.ToString());
                }
                List<UIElement> PinToDelete = new List<UIElement>();

                for (int i = 0; i < MyMap.Children.Count; i++)
                {
                    if (MyMap.Children[i] is Pushpin && ListTotalPoint.Contains((MyMap.Children[i] as Pushpin).Tag.ToString()))
                    {
                        PinToDelete.Add(MyMap.Children[i]);
                    }
                    if (MyMap.Children[i] is MapShapeBase && ListTotalPoint.Contains((MyMap.Children[i] as MapShapeBase).Tag.ToString()))
                    {
                        PinToDelete.Add(MyMap.Children[i]);
                    }
                }

                foreach (var el in PinToDelete)
                    MyMap.Children.Remove(el);
            }

            listBox.Items.Refresh();
        }

        private void Polyline_Click(object sender, RoutedEventArgs e) //APRES AVOIR SELECTIONNER LES ELEMENTS SUR LA LISTE, ON CLICK SUR POLYLINE ET LA POLYLINE EST CONSTRUITE 
        {
            MyCartographyObjects.Polyline line = new MyCartographyObjects.Polyline(LineP);
            if (listBox.SelectedItems.Count < 2)
            {
                return;
            }

            var selectedElems = listBox.SelectedItems;
            List<int> ListIdDelete = new List<int>();

            for (int i = 0; i < selectedElems.Count; i++)
            {
                ListIdDelete.Add((selectedElems[i] as Coordonnees).Id);
                line.coord.Add(selectedElems[i] as Coordonnees);
            }

            DrawLine(line.coord, line.Id, LineP);
            foreach (var selid in ListIdDelete)
            {
                _myMapData.ObservableCollection.Remove(_myMapData.ObservableCollection.Cast<CartoObj>().First(a => a.Id == selid));
            }
            _myMapData.ObservableCollection.Add(line);
            listBox.Items.Refresh();
        }

        private void Polygone_Button_Click(object sender, RoutedEventArgs e)//APRES AVOIR SELECTIONNER LES ELEMENTS SUR LA LISTE, ON CLICK SUR POLYGONE ET LE POLYGONE EST CONSTRUIT 
        {
            Polygone gone = new Polygone(LineP, BackP);
            if (listBox.SelectedItems.Count <= 1)
            {
                return;
            }
            var selectedElems = listBox.SelectedItems;
            List<int> ListIdDelete = new List<int>();
            for (int i = 0; i < listBox.SelectedItems.Count; i++)
            {
                ListIdDelete.Add((selectedElems[i] as Coordonnees).Id);
                gone.coord.Add(listBox.SelectedItems[i] as Coordonnees);
            }

            DrawGone(gone.coord, gone.Id, gone.Contour, gone.Remplissage);
            foreach (var selid in ListIdDelete)
                _myMapData.ObservableCollection.Remove(_myMapData.ObservableCollection.Cast<CartoObj>().First(a => a.Id == selid));
            _myMapData.ObservableCollection.Add(gone);
            listBox.Items.Refresh();

        }
        #endregion
        #region TOOLS
        public void MenuToolsOption_Click(object sender, RoutedEventArgs e) //AFFICHE LE MENU OPTIONS , MODIFIE LES COULEURS PAR DEFAUT 
        {
            opW.Owner = this;
            opW.Ok_Button.Click += OpW_Ok;
            opW.Appliquer_Button.Click += OpW_Appliquer;
            opW.Closing += OpW_Closing;
            opW.Show();

        }

        private void OpW_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            opW.Hide();
        }
        private void OpW_Appliquer(object sender, EventArgs e)
        {
            ListBoxColorChanged();
        }

        private void OpW_Ok(object sender, EventArgs e)
        {
            ListBoxColorChanged();
        }

        private void ListBoxColorChanged()
        {
            //DossierTravail = "nouveau";
            listBox.Background = new SolidColorBrush(opW.back);
            listBox.Foreground = new SolidColorBrush(opW.line);

            LineP = opW.linePoly;
            BackP = opW.backPoly;
        }

        private void MenuToolsAboutBox_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("My Cartography Objects\nOvali Tolga 2225",
                                          "About this app",
                                          MessageBoxButton.OK);
        }
        #endregion
        #region MENU
        private void MenuFileOpen_Click(object sender, RoutedEventArgs e)//OUVRE UN 
        {
            string filename = "";

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".dat";
            dlg.Filter = "DAT Files (*.dat)|*.dat";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                filename = dlg.FileName;

                _myMapData = new MyPersonalMapData().LoadAsBinary(filename);
                Map m2 = new Map(_myMapData);
                m2.Show();
                this.Close();

                listBox.Items.Refresh();
                LoadCoordoonees();
            }

        }
        private void MenuFileSave_Click(object sender, RoutedEventArgs e)//SAUVEGARDE LE FICHIER AVEC LE NOM ET LE PRENOM COMME FILENAME
        {
            _myMapData.SaveAsBinary(DossierTravail);
        }
        private void MenuFilePOIImport_Click(object sender, RoutedEventArgs e) //IMPORTE UN FICHIER DE POIs
        {
            string filename = "";

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".csv";
            dlg.Filter = "CSV Files (*.csv)|*.csv";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                filename = dlg.FileName;

                using (TextFieldParser parser = new TextFieldParser(@filename))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(";");

                    POI poi = new POI();

                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();

                        for (int i = 2; i <= fields.Length; i += 3)
                        {
                            poi = new POI(fields[i], Double.Parse(fields[i - 2]), Double.Parse(fields[i - 1]));

                            Pushpin pin = new Pushpin();
                            pin.Location = new Location(Double.Parse(fields[i - 2]), Double.Parse(fields[i - 1]));

                            // MyMap.Children.Add(pin);
                            AddPushPin(poi);
                            _myMapData.ObservableCollection.Add(poi);
                        }
                    }
                }
                listBox.Items.Refresh();
            }
        }
        private void MenuFilePOIExport_Click(object sender, RoutedEventArgs e)//EXPORTE TOUT LES POIs PRESENTS DANS LES DONNEES
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

            dlg.DefaultExt = ".csv";
            dlg.Filter = "CSV Files (*.csv)|*.csv";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;

                StreamWriter sw = new StreamWriter(filename, false);
                // POI poi = _myMapData.ObservableCollection[0] as POI;
                foreach (ICartoObj car in _myMapData.ObservableCollection)
                {
                    if (car is POI)
                    {
                        POI poi = car as POI;
                        sw.WriteLine(poi.Latitude + ";" + poi.Longitude + ";" + poi.Description + ";");
                    }
                }

                sw.Close();
            }
        }
        private void MenuFileTrajetImport_Click(object sender, RoutedEventArgs e)//IMPORTE UNE POLYLINE D'UN FICHIER CSV
        {
            string filename = "";

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".csv";
            dlg.Filter = "CSV Files (*.csv)|*.csv";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                filename = dlg.FileName;

                using (TextFieldParser parser = new TextFieldParser(@filename))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(";");

                    POI poi = new POI();
                    MyCartographyObjects.Polyline line = new MyCartographyObjects.Polyline();
                    List<Coordonnees> c = new List<Coordonnees>();
                    LocationCollection loc = new LocationCollection();

                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();

                        for (int i = 2; i <= fields.Length; i += 3)
                        {
                            poi = new POI(fields[i], Double.Parse(fields[i - 2]), Double.Parse(fields[i - 1]));

                            Pushpin pin = new Pushpin();
                            pin.Location = new Location(Double.Parse(fields[i - 2]), Double.Parse(fields[i - 1]));

                            if (String.IsNullOrEmpty(poi.Description))
                            {
                                c.Add(new Coordonnees(poi.Latitude, poi.Longitude));
                                loc.Add(new Location(poi.Latitude, poi.Longitude));
                            }
                            else
                            {

                                _myMapData.ObservableCollection.Add(poi);
                            }
                            i += 3;
                        }
                    }

                    if (loc.Count() > 0)
                    {
                        line = new MyCartographyObjects.Polyline(c, LineP);
                        foreach (var point in line.coord)
                        {
                            AddPushPin(point);
                        }
                        DrawLine(line.coord, line.Id, line.Colrs);

                        _myMapData.ObservableCollection.Add(line);
                    }
                }
            }
            listBox.Items.Refresh();
        }
        private void MenuFileTrajetExport_Click(object sender, RoutedEventArgs e)//EXPORTE UNE SEULE POYLINE DES DONNES DANS UN FICHIER CSV
        {
            int cpt = 0;
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

            dlg.DefaultExt = ".csv";
            dlg.Filter = "CSV Files (*.csv)|*.csv";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;

                StreamWriter sw = new StreamWriter(filename, false);
                // POI poi = _myMapData.ObservableCollection[0] as POI;
                foreach (ICartoObj car in _myMapData.ObservableCollection)
                {
                    if (car is MyCartographyObjects.Polyline && cpt==0)
                    {
                        MyCartographyObjects.Polyline line = car as MyCartographyObjects.Polyline;
                        //sw.WriteLine(line.Id + ";" + line.Colrs + ";");
                        foreach (var cord in line.coord)
                        {
                            if (cord is POI)
                            {
                                POI poin = cord as POI;
                                sw.WriteLine(poin.Latitude + ";" + poin.Longitude + ";" + poin.Description + ";");
                            }
                            else
                                sw.WriteLine(cord.Latitude + ";" + cord.Longitude + ";");

                        }
                        cpt++;
                    }

                }
                cpt = 0;
                sw.Close();

            }
        }
        private void MenuExit_Click(object sender, RoutedEventArgs e)//EXIT
        {
            System.Windows.Application.Current.Shutdown();
        }
        #endregion
    }
}

