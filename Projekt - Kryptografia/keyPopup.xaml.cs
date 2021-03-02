using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Projekt___Kryptografia
{
	/// <summary>
	/// Logika interakcji dla klasy keyPopup.xaml
	/// </summary>
	public partial class keyPopup : Window
	{
		bool clicked = false;
		public string iKey;
		public keyPopup()
		{
			InitializeComponent();
		}

		private void textBox_Click(object sender, MouseEventArgs e)
		{
			if (!clicked)
			{
				clicked = true;
				telephoneNumber.Clear();
				telephoneNumber.Foreground = new SolidColorBrush(Colors.Black);
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (telephoneNumber.Text.Length < 3)
			{
				MessageBox.Show("The key must be at least 3 characters long");
				return;
			}
			iKey = telephoneNumber.Text;
			this.Close();
		}
	}
}
