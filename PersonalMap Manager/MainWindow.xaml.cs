using MyCartographyObjects;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Globalization;

namespace PersonalMap_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int i;
        public POI point;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonEncode_Click_1(object sender, RoutedEventArgs e)
        {
            string x = TextBoxLatitude.Text;
            string y = TextBoxLongitude.Text;
            string desc = TextBoxDescription.Text;

            point = new POI(desc, Convert.ToDouble(x, CultureInfo.InvariantCulture), Convert.ToDouble(y, CultureInfo.InvariantCulture));
            i++;
            this.DialogResult = true;
            this.Close();


        }
    }
}
