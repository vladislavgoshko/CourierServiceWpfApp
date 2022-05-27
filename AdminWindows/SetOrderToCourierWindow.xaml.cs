using System.Windows;
using CourierServiceLibrary.Models;
using CourierServiceLibrary;
using CourierServiceWpfApp.UserControls;
using CourierServiceWpfApp.MainWindows;

namespace CourierServiceWpfApp.AdminWindows
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
                    OrdersStackPanel.Children.Add(new OrderControl(this, order, i++));
            }
        }
    }
}
