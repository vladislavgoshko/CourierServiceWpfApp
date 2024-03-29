﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace CourierServiceWpfApp.MainWindows
{
    /// <summary>
    /// Логика взаимодействия для Map.xaml
    /// </summary>
    public partial class Map : Window
    {
        TextBox X;
        TextBox Y;
        public Map(TextBox x, TextBox y)
        {
            InitializeComponent();
            X = x;
            Y = y;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (x.Text == "" || y.Text == "")
                MessageBox.Show("Вы не выбрали местоположение");
            else
            {
                X.Text = x.Text;
                Y.Text = y.Text;
                Close();
            }
        }

        private void MapImage_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            x.Text = e.GetPosition(MapImage).X.ToString();
            y.Text = e.GetPosition(MapImage).Y.ToString();
        }
    }
}
