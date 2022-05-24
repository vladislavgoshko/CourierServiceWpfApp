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
using CourierServiceLibrary;
using CourierServiceLibrary.Models;
using CourierServiceWpfApp.UserControls;

namespace CourierServiceWpfApp
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public UserWindow()
        {
            InitializeComponent();
            DisplayOrders();
            if (Authentification.signedUser.Role == UserRole.Client)
                HideElementsForClients();
            else
            {
                DisplayCouriers();
            }
        }
        private void HideElementsForClients()
        {
            AddingCourier.Visibility = Visibility.Hidden;
        }
        internal void DisplayOrders()
        {
            int i = 1;
            OrdersStackPanel.Children.Clear();
            foreach (Order order in ProjectResources.Instance.OrdersRepository.Read())
            {
                if(Authentification.signedUser.Role == UserRole.Admin)
                {
                    OrdersStackPanel.Children.Add(new OrderControl(this, order, i) { SetCourierButton = { Visibility = Visibility.Hidden }, ShowMessageButton = { IsEnabled = true } });
                    i++;
                }
                else
                {
                    if (order.UserId == Authentification.signedUser.Id)
                    {
                        OrdersStackPanel.Children.Add(new OrderControl(this, order, i) { SetCourierButton = { Visibility = Visibility.Hidden } });
                        i++;
                    }
                }
               
            }
        }
        internal void DisplayCouriers()
        {
            int i = 1;
            CouriersStackPanel.Children.Clear();
            foreach (Courier courier in ProjectResources.Instance.CouriersRepository.Read())
            {
                CouriersStackPanel.Children.Add(new CourierControl(this, courier, i));
                i++;
            }
        }
        private void SetStartLocationButton_Click(object sender, RoutedEventArgs e)
        {
            new Map(StartLocationXTextBox, StartLocationYTextBox).Show();
        }
        private void SetEndLocationButton_Click(object sender, RoutedEventArgs e)
        {
            new Map(EndLocationXTextBox, EndLocationYTextBox).Show();
        }
        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (StartLocationXTextBox.Text == "" || EndLocationXTextBox.Text == "")
            {
                MessageBox.Show("Укажите точки маршрута");
            }
            else if (!Double.TryParse(WeightTextBox.Text, out double weight) || !Double.TryParse(HeightTextBox.Text, out double height)
                || !Double.TryParse(WidthTextBox.Text, out double width) || !Double.TryParse(LengthTextBox.Text, out double length))
            {
                MessageBox.Show("Введите корректные значения");
            }
            else
            {
                ProjectResources.Instance.OrdersRepository.Create(new Order
                {
                    Readiness = Readiness.Pending,
                    StartCoords = new double[] { Convert.ToDouble(StartLocationXTextBox.Text), Convert.ToDouble(StartLocationYTextBox.Text) },
                    EndCoords = new double[] { Convert.ToDouble(EndLocationXTextBox.Text), Convert.ToDouble(EndLocationYTextBox.Text) },
                    Height = Convert.ToDouble(HeightTextBox.Text),
                    Width = Convert.ToDouble(WidthTextBox.Text),
                    Length = Convert.ToDouble(LengthTextBox.Text),
                    Weigth = Convert.ToDouble(WeightTextBox.Text),
                    UserId = Authentification.signedUser.Id,
                    Created = DateTime.Now,
                    CourierId = -1
                }
                );
                MessageBox.Show("Закак оформлен. Постараемся как можно быстрее выполнить его");
                StartLocationXTextBox.Clear();
                StartLocationYTextBox.Clear();
                EndLocationXTextBox.Clear();
                EndLocationYTextBox.Clear();
                HeightTextBox.Clear();
                WidthTextBox.Clear();
                LengthTextBox.Clear();
                WeightTextBox.Clear();
                DisplayOrders();
            }

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayOrders();
            if (Authentification.signedUser.Role == UserRole.Admin)
                DisplayCouriers();
        }

        private void UnSignButton_Click(object sender, RoutedEventArgs e)
        {
            ProjectResources.Instance.authentification.UnSign();
            new AuthentificationWindow().Show();
            Close();
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить свой профиль?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                ProjectResources.Instance.UsersRepository.Delete(Authentification.signedUser.Id);
                ProjectResources.Instance.authentification.UnSign();
                new AuthentificationWindow().Show();
                Close();
            }
        }

        private void AddCourierButton_Click(object sender, RoutedEventArgs e)
        {
            new AddingNewCourier(this)
            {
                TypeCourierComboBox =  { ItemsSource = Enum.GetValues(typeof(TransportType)) }
            }.Show();
        }
    }
}
