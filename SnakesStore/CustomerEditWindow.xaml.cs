using SnakesStore.Models;
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
    /// Логика взаимодействия для CustomerEditWindow.xaml
    /// </summary>
    public partial class CustomerEditWindow : Window
    {
        private Customer _customer;
        
        public CustomerEditWindow()
        {
            InitializeComponent();
            _customer = new Customer(); // Создаем новый объект для добавления
            DataContext = _customer;
        }
        public CustomerEditWindow(Customer customer)
        {
            InitializeComponent();
            _customer = customer; // Передаем существующего клиента для редактирования
            DataContext = _customer;
            FirstNameTextBox.Text = _customer.FirstName;
            LastNameTextBox.Text = _customer.LastName;
            EmailTextBox.Text = _customer.Email;
            PhoneTextBox.Text = _customer.Phone ?? string.Empty;  // Обработка возможного null значения
            AddressTextBox.Text = _customer.Address ?? string.Empty; // Обработка возможного null значения
        }

        // Обработчик для кнопки "Save"
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(LastNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                MessageBox.Show("Все поля обязательны для заполнения!");
                return;
            }

            try
            {
                // Заполняем поля клиента данными из TextBox'ов
                _customer.FirstName = FirstNameTextBox.Text;
                _customer.LastName = LastNameTextBox.Text;
                _customer.Email = EmailTextBox.Text;
                _customer.Phone = PhoneTextBox.Text;
                _customer.Address = AddressTextBox.Text;

                var context = new SnackStoreContext();

                if (_customer.CustomerId == 0) // Новый клиент
                {
                    context.Customers.Add(_customer);
                }
                else // Редактируем существующего клиента
                {
                    var existingCustomer = context.Customers
                                                  .FirstOrDefault(c => c.CustomerId == _customer.CustomerId);

                    if (existingCustomer != null)
                    {
                        // Обновляем данные существующего клиента
                        existingCustomer.FirstName = _customer.FirstName;
                        existingCustomer.LastName = _customer.LastName;
                        existingCustomer.Email = _customer.Email;
                        existingCustomer.Phone = _customer.Phone;
                        existingCustomer.Address = _customer.Address;

                        context.Customers.Update(existingCustomer);
                    }
                    else
                    {
                        MessageBox.Show("Клиент не найден!");
                        return;
                    }
                }

                context.SaveChanges();
                DialogResult = true; // Закрываем окно с успешным результатом
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        // Обработчик для кнопки "Отменить"
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // Закрываем окно без изменений
        }
    }
}

