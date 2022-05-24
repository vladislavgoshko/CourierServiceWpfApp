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
using CourierServiceLibrary.Models;
using CourierServiceLibrary.Repository;
using CourierServiceLibrary;

namespace CourierServiceWpfApp
{
    /// <summary>
    /// Логика взаимодействия для SendMessageWindow.xaml
    /// </summary>
    public partial class SendMessageWindow : Window
    {
        private UserWindow _userWindow;
        private Order _order;
        public SendMessageWindow(UserWindow userWindow, Order order)
        {
            InitializeComponent();
            _userWindow = userWindow;
            _order = order;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _order.Message = MessageTextBox.Text;
            ProjectResources.Instance.OrdersRepository.Update(_order);
            _userWindow.DisplayOrders();
            Close();
        }
    }
}
