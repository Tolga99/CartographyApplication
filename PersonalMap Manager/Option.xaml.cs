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
	/// Interaction logic for Option.xaml
	/// </summary>
	public delegate void ColorSelected(object sender, EventArgs e);

	public partial class Option : Window
	{
		public Color line;
		public Color back;

		public Color linePoly = Colors.Black;
		public Color backPoly = Colors.Gold;
		public Option()
		{
			InitializeComponent();
		}

		private void Ok_Button_Click(object sender, RoutedEventArgs e)
		{
			if (ColorP_Line.SelectedColor.HasValue is true)
				line = ColorP_Line.SelectedColor.Value;
			else line = Colors.Black;
			if (ColorP_Back.SelectedColor.HasValue is true)
				back = ColorP_Back.SelectedColor.Value;
			else back = Colors.White;

			if (ColorP_LineP.SelectedColor.HasValue is true)
				linePoly = ColorP_LineP.SelectedColor.Value;
			else linePoly = Colors.Black;
			if (ColorP_BackP.SelectedColor.HasValue is true)
				backPoly = ColorP_BackP.SelectedColor.Value;
			else backPoly = Colors.Gold;
			this.Close();
		}

		private void Appliquer_Button_Click(object sender, RoutedEventArgs e)
		{
			if (ColorP_Line.SelectedColor.HasValue is true)
				line = ColorP_Line.SelectedColor.Value;
			else line = Colors.Black;
			if (ColorP_Back.SelectedColor.HasValue is true)
				back = ColorP_Back.SelectedColor.Value;
			else back = Colors.White;

			if (ColorP_LineP.SelectedColor.HasValue is true)
				linePoly = ColorP_LineP.SelectedColor.Value;
			else linePoly = Colors.Black;
			if (ColorP_BackP.SelectedColor.HasValue is true)
				backPoly = ColorP_BackP.SelectedColor.Value;
			else backPoly = Colors.Gold;
		}
		private void OnAppliquer(object sender, RoutedEventArgs e)
		{
			this.Activate();
		}
		private void Cancel_Button_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
