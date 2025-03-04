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
    /// Логика взаимодействия для OrderEditWindow.xaml
    /// </summary>
    public partial class OrderEditWindow : Window
    {
        private Order _order;
        
        public OrderEditWindow()
        {
            InitializeComponent();
            _order = new Order(); // Создаем новый заказ
            DataContext = _order;
        }
        public OrderEditWindow(Order order)
        {
            InitializeComponent();
            _order = order; // Передаем существующий заказ
            DataContext = _order;
            // Заполняем поля данными из существующего заказа
            /*CustomerTextBox.Text = _order.Customer.FullName;*/ // Здесь предполагаем, что у клиента есть свойство FullName
            TotalAmountTextBox.Text = _order.TotalAmount.ToString();
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TotalAmountTextBox.Text))
            {
                MessageBox.Show("Поле общей суммы обязательно для заполнения!");
                return;
            }

            try
            {
                // Заполняем поля заказа данными из TextBox'ов
                _order.TotalAmount = decimal.Parse(TotalAmountTextBox.Text);
                var context = new SnackStoreContext();

                if (_order.OrderId == 0) // Новый заказ
                {
                    // Допустим, у нас есть выбор клиента, который нужно записать
                    var selectedCustomerId = _order.CustomerId; // Нужно передать правильного клиента
                    var customer = context.Customers.FirstOrDefault(c => c.CustomerId == selectedCustomerId);
                    if (customer != null)
                    {
                        _order.Customer = customer;
                    }
                    context.Orders.Add(_order);
                }
                else // Редактируем существующий заказ
                {
                    var existingOrder = context.Orders
                                                .FirstOrDefault(o => o.OrderId == _order.OrderId);

                    if (existingOrder != null)
                    {
                        // Обновляем данные существующего заказа
                        existingOrder.TotalAmount = _order.TotalAmount;
                        existingOrder.CustomerId = _order.CustomerId;
                        context.Orders.Update(existingOrder);
                    }
                    else
                    {
                        MessageBox.Show("Заказ не найден!");
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
