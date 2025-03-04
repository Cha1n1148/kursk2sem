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

namespace SnakesStore
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            // Проверка введенных данных
            if (IsValidUser(username, password))
            {
                MessageBox.Show("Вы вошли!");

                // Открываем главное окно
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close(); // Закрываем окно авторизации
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль.");
            }
        }

        // Простейшая проверка двух пользователей: admin и client
        private bool IsValidUser(string username, string password)
        {
            // Проверяем, является ли введенные логин и пароль валидными
            if ((username == "admin" && password == "123") || (username == "client" && password == "321"))
            {
                return true;
            }

            return false;
        }
    }
}

