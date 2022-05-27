using System;
using System.Windows;
using System.Windows.Controls;using CourierServiceLibrary;
using CourierServiceLibrary.Models;
using CourierServiceWpfApp.MainWindows;

namespace CourierServiceWpfApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (LoginTextBox.Text == "" && PasswordBox.Password == "")
                {
                    throw new Exception("Нужно заполнить все поля для входа");
                }
                else
                {
                    ProjectResources.Instance.authentification.SignIn(LoginTextBox.Text, PasswordBox.Password);
                    new UserWindow
                    {
                        NameLabel = {Content = Authentification.signedUser.Name},
                        SecNameLabel = {Content = Authentification.signedUser.SecondName},
                        LoginLabel = {Content = Authentification.signedUser.Login},
                        IdLabel = {Content = Authentification.signedUser.Id},
                    }.Show();

                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RegisterLoginTextBox.Text == "" || RegisterNameTextBox.Text == "" || RegisterSecondNameTextBox.Text == "" || RegisterPasswordBox.Password == "")
                    throw new Exception("Нужно заполнить все поля для регистрации");
                ProjectResources.Instance.UsersRepository.Create(new User
                {
                    Role = UserRole.Client,
                    Name = RegisterNameTextBox.Text,
                    SecondName = RegisterSecondNameTextBox.Text,
                    Login = RegisterLoginTextBox.Text,
                    Password = RegisterPasswordBox.Password

                });
                MessageBox.Show("Вы успешно зарегистрировались");
                RegisterLoginTextBox.Clear();
                RegisterNameTextBox.Clear();
                RegisterPasswordBox.Clear();
                RegisterSecondNameTextBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
