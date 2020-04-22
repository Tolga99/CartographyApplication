using MyCartographyObjects;
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

namespace PersonalMap_Manager
{
    /// <summary>
    /// Interaction logic for FirstW.xaml
    /// </summary>
    public partial class FirstW : Window
    {
        public FirstW()
        {
            InitializeComponent();
        }

        private void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            string nom = TextBoxNom.Text;
            string prenom = TextBoxPrenom.Text;
            string email = TextBoxMail.Text;

            MyPersonalMapData data = new MyPersonalMapData(nom, prenom, email);
            if (data.BinaryExist())
            {
                data = data.LoadAsBinary();
            }
            else
            {
                data.SaveAsBinary();
            }
            Map m = new Map(data);
            m.Show();
            this.Close();
        }

        private void CancelButtonFW_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
