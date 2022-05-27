using System;
using System.Windows;
using System.Windows.Controls;using CourierServiceLibrary;
using CourierServiceLibrary.Models;
using CourierServiceWpfApp.AdminWindows;
using CourierServiceWpfApp.MainWindows;

namespace CourierServiceWpfApp.UserControls
{
    /// <summary>
    /// Логика взаимодействия для OrderControl.xaml
    /// </summary>
    public partial class CourierControl : UserControl
    {
        private Courier _courier;
        private UserWindow _userWindow;
        
        public CourierControl(UserWindow userWindow,Courier courier, int number)
        {
            InitializeComponent();
            _courier = courier;
            _userWindow = userWindow;
            CourierNumberLabel.Content += number.ToString();
            NameLabel.Content += courier.Name;
            SecondNameLabel.Content += courier.SecondName;
            AdressXTextBox.Text = courier.Address[0].ToString();
            AdressYTextBox.Text = courier.Address[1].ToString();
            TypeLabel.Content += Courier.TypeToString(courier.TransportType);
            BusinessLabel.Content += courier.IsFreeAndWhen(out DateTime _) ? "Свободен" : "Выполняет";
        }
        /// <summary>
        /// Удаление курьера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteCourierButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить курьера?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                ProjectResources.Instance.CouriersRepository.Delete(_courier.Id);
                _userWindow.DisplayCouriers();
            }
        }
        /// <summary>
        /// Открытие окна окна со списком назначенных курьеру заказов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowOrdersListButton_Click(object sender, RoutedEventArgs e)
        {
            new TimetableOfCourier(_userWindow, _courier).Show();
        }
        /// <summary>
        /// Назначение курьеру заказа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetOrderButton_Click(object sender, RoutedEventArgs e)
        {
            new SetOrderToCourierWindow(_userWindow, _courier).Show();
        }
    }
}
