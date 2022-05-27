using System;
using System.Windows;
using System.Windows.Controls;
using CourierServiceLibrary;
using CourierServiceLibrary.Models;
using CourierServiceWpfApp.AdminWindows;
using CourierServiceWpfApp.MainWindows;

namespace CourierServiceWpfApp.UserControls
{
    /// <summary>
    /// Логика взаимодействия для OrderControl.xaml
    /// </summary>
    public partial class OrderControl : UserControl
    {
        private Order _order;
        private UserWindow _userWindow;
        private SetOrderToCourierWindow _setOrderToCourierWindow;
        public OrderControl(UserWindow userWindow, Order order, int number)
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
        public OrderControl(SetOrderToCourierWindow setOrderToCourierWindow, Order order, int number) : this(setOrderToCourierWindow._userWindow, order, number)
        {
            _setOrderToCourierWindow = setOrderToCourierWindow;
        }
        /// <summary>
        /// Удаление заказа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Отображение сообщения от администрации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowMessageButton_Click(object sender, RoutedEventArgs e)
        {
            if (Authentification.signedUser.Role == UserRole.Client)
            {
                if (_order.Message == null || _order.Message == "")
                    MessageBox.Show("Вам ничего не писали");
                else
                    MessageBox.Show(_order.Message, "У вас сообщение от администрации:");
            }
            else
                new SendMessageWindow(_userWindow, _order).Show();
        }
        /// <summary>
        /// Назначение заказа курьеру
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetOrderToCourierButton_Click(object sender, RoutedEventArgs e)
        {
            int startHourWorkDay = Convert.ToInt32(_userWindow.StartTimeWorkTextBox.Text),
                endHourWorkDay = Convert.ToInt32(_userWindow.EndTimeWorkTextBox.Text);

            if (!_setOrderToCourierWindow._courier.AddOrder(out string message, startHourWorkDay, endHourWorkDay, _order))
                MessageBox.Show(message);
            else
            {
                _userWindow.DisplayCouriers();
                _userWindow.DisplayOrders();
                _setOrderToCourierWindow.Close();
            }
        }
    }
}
