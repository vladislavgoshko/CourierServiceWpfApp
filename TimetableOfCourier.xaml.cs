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
using CourierServiceLibrary;
using CourierServiceWpfApp.UserControls;

namespace CourierServiceWpfApp
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
            foreach (int id in courier.ListOrdersId)
                foreach (Order order in orders)
                    if (order.Id == id)
                        OrdersStackPanel.Children.Add(new OrderControl(userWindow, order, i) { SetCourierButton = { Visibility = Visibility.Hidden } });
        }
    }
}
