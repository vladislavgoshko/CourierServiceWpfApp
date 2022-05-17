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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CourierServiceLibrary;
using CourierServiceLibrary.Models;

namespace CourierServiceWpfApp
{
    /// <summary>
    /// Логика взаимодействия для OrderControl.xaml
    /// </summary>
    public partial class OrderControl : UserControl
    {
        private Order _order;
        private UserWindow _userWindow;
        public OrderControl(UserWindow userWindow,Order order, int number)
        {
            InitializeComponent();
            _order = order;
            OrderNumberLabel.Content += number.ToString();
            StateOrderLabel.Content += order.ReadinessToString();
            WeightLabel.Content += order.Weigth.ToString() + " г";
            SizesLabel.Content += $"{order.Height}мм*{order.Height}мм*{order.Width}мм";
            TimeCreatedLabel.Content += order.Created.ToString("g");
            DistanceLabel.Content += Math.Round(order.GetDistance(), 2).ToString();
            _userWindow = userWindow;
            if (order.Message == null || order.Message == "")
                ShowMessageButton.IsEnabled = false;
        }

        private void DeleteOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (_order.Readiness == Readiness.Pending)
            {
                ProjectResources.Instance.OrdersRepository.Delete(_order.Id);
                _userWindow.DisplayOrders();
            }
            else
                MessageBox.Show("Нельзя удалить заказ");
        }
        private void ShowMessageButton_Click(object sender, RoutedEventArgs e)
        {
            if (_order.Message == null || _order.Message == "")
                MessageBox.Show("Вам ничего не писали");
            else
                MessageBox.Show(_order.Message, "У вас сообщение от администрации:");
        }
    }
}
