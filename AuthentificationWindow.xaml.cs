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

namespace CourierServiceWpfApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class AuthentificationWindow : Window
    {
        public AuthentificationWindow()
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
                    Name = LoginTextBox.Text,
                    SecondName = LoginTextBox.Text,
                    Login = RegisterLoginTextBox.Text,
                    Password = RegisterPasswordBox.Password

                });
                //ProjectResources.Instance.Service.SignIn(RegisterLoginTextBox.Text, RegisterPasswordBox.Password);
                MessageBox.Show("Вы успешно зарегистрировались");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
