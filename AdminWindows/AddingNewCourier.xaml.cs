using System;
using System.Windows;
using CourierServiceLibrary;
using CourierServiceLibrary.Models;
using CourierServiceWpfApp.MainWindows;

namespace CourierServiceWpfApp.AdminWindows
{
    /// <summary>
    /// Логика взаимодействия для AddingNewCourier.xaml
    /// </summary>
    public partial class AddingNewCourier : Window
    {
        private UserWindow _userWindow { get; set; }
        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        /// <param name="userWindow"></param>
        public AddingNewCourier(UserWindow userWindow)
        {
            InitializeComponent();
            _userWindow = userWindow;
        }
        private void SetAdressButton_Click(object sender, RoutedEventArgs e)
        {
            new Map(AdressXTextBox, AdressYTextBox).Show();
        }

        private void AddCourierButton_Click(object sender, RoutedEventArgs e)
        {
            if (AdressXTextBox.Text == "")
            {
                MessageBox.Show("Укажите адрес");
            }
            else if (CourierNameTextBox.Text == "" || CourierSecNameTextBox.Text == "" || TypeCourierComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Заполните все поля");
            }
            else
            {
                ProjectResources.Instance.CouriersRepository.Create(new Courier
                {
                    Name = CourierNameTextBox.Text,
                    SecondName = CourierSecNameTextBox.Text,
                    Address = new double[] { Convert.ToDouble(AdressXTextBox.Text), Convert.ToDouble(AdressYTextBox.Text) },
                    LastLocation = new double[] { Convert.ToDouble(AdressXTextBox.Text), Convert.ToDouble(AdressYTextBox.Text) },
                    TransportType = (TransportType)TypeCourierComboBox.SelectedIndex
                }
                );
                MessageBox.Show("Курьер успешно добавлен");
                _userWindow.DisplayCouriers();
                Close();
            }
        }
    }
}
