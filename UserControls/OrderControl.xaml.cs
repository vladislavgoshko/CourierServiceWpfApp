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
        public OrderControl(SetOrderToCourierWindow setOrderToCourierWindow, Order order, int number) : this (setOrderToCourierWindow._userWindow, order, number)
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
            Courier courier = _setOrderToCourierWindow._courier;
            int startHour = Convert.ToInt32(_userWindow.StartTimeWorkTextBox.Text), 
                endHour = Convert.ToInt32(_userWindow.EndTimeWorkTextBox.Text);

            double minutesToCompletingOrder = (courier.GetDistanceFromEndLocation(_order.StartCoords) + _order.GetDistance()) / courier.Speed * 60;
            if (minutesToCompletingOrder < (endHour - startHour) * 60)
            {
                courier.IsFreeAndWhen(out DateTime when);
                if (DateTime.Now.Date == when.Date)
                {
                    when = when.AddMinutes(minutesToCompletingOrder);
                    if (when.Hour < endHour && when.Hour > startHour)
                    {
                        _order.Readiness = Readiness.InProgress;
                        _order.CourierId = courier.Id;

                        _order.StartedExecuting = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, when.Hour, when.Minute, 0);
                        courier.ListOrdersId.Add(_order.Id);
                        courier.LastLocation = _order.EndCoords;

                        ProjectResources.Instance.OrdersRepository.Update(_order);
                        ProjectResources.Instance.CouriersRepository.Update(courier);
                        _userWindow.DisplayCouriers();
                        _userWindow.DisplayOrders();
                        _setOrderToCourierWindow.Close();
                    }
                    else
                        MessageBox.Show("Курьер не успеет выполнить заказ до конца рабочего дня");
                }
                else
                    MessageBox.Show("Курьер не успеет выполнить заказ до конца рабочего дня");
            }
            else
                MessageBox.Show("Заказ невозможно выполнить за целый рабочий день");
        }
    }
}
