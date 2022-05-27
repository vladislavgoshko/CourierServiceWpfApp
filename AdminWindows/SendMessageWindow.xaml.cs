using System.Windows;
using CourierServiceLibrary.Models;
using CourierServiceLibrary;
using CourierServiceWpfApp.MainWindows;

namespace CourierServiceWpfApp.AdminWindows
{
    /// <summary>
    /// Логика взаимодействия для SendMessageWindow.xaml
    /// </summary>
    public partial class SendMessageWindow : Window
    {
        private UserWindow _userWindow;
        private Order _order;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="userWindow"></param>
        /// <param name="order"></param>
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
