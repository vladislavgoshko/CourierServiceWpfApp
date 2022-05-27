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

namespace CourierServiceWpfApp
{
    /// <summary>
    /// Логика взаимодействия для Map.xaml
    /// </summary>
    public partial class Map : Window
    {
        TextBox X { get; set; }
        TextBox Y { get; set; }
        public Map(ref TextBox x, ref TextBox y)
        {
            InitializeComponent();
            X = x;
            Y = y;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            X = x;
            Y = y;
            Close();
        }
    }
}
