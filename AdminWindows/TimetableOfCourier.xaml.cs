using System.Collections.Generic;
using System.Windows;
using CourierServiceLibrary.Models;
using CourierServiceLibrary;
using CourierServiceWpfApp.UserControls;
using CourierServiceWpfApp.MainWindows;
using System.Linq;

namespace CourierServiceWpfApp.AdminWindows
{
    /// <summary>
    /// Логика взаимодействия для TimetableOfCourier.xaml
    /// </summary>
    public partial class TimetableOfCourier : Window
    {
        public TimetableOfCourier(UserWindow userWindow,Courier courier)
        {
            InitializeComponent();
            int i = 1;
            IEnumerable<Order> orders = ProjectResources.Instance.OrdersRepository.Read();
            IEnumerable<Order> items = from id in courier.ListOrdersId
                        join order in orders
                        on id equals order.Id
                        select order;
            foreach (Order item in items)
                OrdersStackPanel.Children.Add(new OrderControl(userWindow, item, i) { SetCourierButton = { Visibility = Visibility.Hidden } });
        }
    }
}
