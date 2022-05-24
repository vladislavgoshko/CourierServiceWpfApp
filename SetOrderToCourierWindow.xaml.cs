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
using CourierServiceWpfApp.UserControls;

namespace CourierServiceWpfApp
{
    /// <summary>
    /// Логика взаимодействия для ListOrdersWindow.xaml
    /// </summary>
    public partial class SetOrderToCourierWindow : Window
    {
        public Courier _courier;
        public UserWindow _userWindow;
        public SetOrderToCourierWindow(UserWindow userWindow, Courier courier)
        {
            InitializeComponent();
            _courier = courier;
            _userWindow = userWindow;
            int i = 1;
            foreach (Order order in ProjectResources.Instance.OrdersRepository.Read())
            {
                if (order.Readiness == Readiness.Pending)
                    OrdersStackPanel.Children.Add(new OrderControl(this ,order, i++));
            }
        }
    }
}
