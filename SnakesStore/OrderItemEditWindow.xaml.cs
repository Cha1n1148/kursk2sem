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
    
    public partial class OrderItemEditWindow : Window
    {
        private OrderItem _orderItem;
        public OrderItemEditWindow()
        {
            InitializeComponent();
            _orderItem = new OrderItem(); // Новый объект для добавления
            DataContext = _orderItem;

            // Заполнение ComboBox с заказами и снэками
            var context = new SnackStoreContext();
            OrderComboBox.ItemsSource = context.Orders.ToList();
            SnackComboBox.ItemsSource = context.Snacks.ToList();
        }
        public OrderItemEditWindow(OrderItem orderItem)
        {
            InitializeComponent();
            _orderItem = orderItem; // Редактирование существующей позиции
            DataContext = _orderItem;

            // Заполнение ComboBox с заказами и снэками
            var context = new SnackStoreContext();
            OrderComboBox.ItemsSource = context.Orders.ToList();
            SnackComboBox.ItemsSource = context.Snacks.ToList();

            // Выбор значений для редактируемого элемента
            OrderComboBox.SelectedValue = _orderItem.OrderId;
            SnackComboBox.SelectedValue = _orderItem.SnackId;
            PriceTextBox.Text = _orderItem.Price?.ToString("F2") ?? string.Empty;
            QuantityTextBox.Text = _orderItem.Quantity.ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrderComboBox.SelectedItem == null || SnackComboBox.SelectedItem == null ||
                string.IsNullOrWhiteSpace(PriceTextBox.Text) || string.IsNullOrWhiteSpace(QuantityTextBox.Text))
            {
                MessageBox.Show("Все поля обязательны для заполнения!");
                return;
            }

            try
            {
                // Заполнение данных из TextBox и ComboBox в объект _orderItem
                _orderItem.OrderId = (int)OrderComboBox.SelectedValue;
                _orderItem.SnackId = (int)SnackComboBox.SelectedValue;
                _orderItem.Price = decimal.Parse(PriceTextBox.Text);
                _orderItem.Quantity = int.Parse(QuantityTextBox.Text);

                var context = new SnackStoreContext();

                if (_orderItem.OrderItemId == 0) // Новый элемент
                {
                    context.OrderItems.Add(_orderItem);
                }
                else // Редактируем существующий элемент
                {
                    var existingOrderItem = context.OrderItems
                        .FirstOrDefault(oi => oi.OrderItemId == _orderItem.OrderItemId);
                    if (existingOrderItem != null)
                    {
                        existingOrderItem.OrderId = _orderItem.OrderId;
                        existingOrderItem.SnackId = _orderItem.SnackId;
                        existingOrderItem.Price = _orderItem.Price;
                        existingOrderItem.Quantity = _orderItem.Quantity;

                        context.OrderItems.Update(existingOrderItem);
                    }
                }

                context.SaveChanges();
                DialogResult = true; // Закрываем окно с успешным результатом
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // Закрыть окно без изменений
        }
    }
}
