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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public UserWindow()
        {
            InitializeComponent();
        }

        private void NumberTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.Key < 34 || (int)e.Key > 43)
                e.Handled = true;
        }

        private void SetStartLocationButton_Click(object sender, RoutedEventArgs e)
        {
            new Map(ref StartLocationXTextBox, ref StartLocationYTextBox).Show();
        }
    }
}
