using Microsoft.EntityFrameworkCore;
using SnakesStore.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SnakesStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private SnackStoreContext _context;
        public MainWindow()
        {
            InitializeComponent();
            _context = new SnackStoreContext();
            LoadOrderItemsData();
            LoadOrdersData();  // Загружаем заказы при запуске окна
            // Загрузка данных при запуске окна
            LoadData();
            
           
        }
        private void LoadData()
        {
            var customers = _context.Customers.ToList();
            var snack = _context.Snacks.ToList();
            CustomersDataGrid.ItemsSource = customers;
            SnacksDataGrid.ItemsSource = snack;
        }

        // Обработчик для кнопки "Добавить"
        private void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new CustomerEditWindow();
            var result = window.ShowDialog(); // Показываем окно как диалог
            if (result == true)
            {
                LoadData(); // Обновляем данные после добавления
            }
        }

        // Обработчик для кнопки "Изменить"
        private void EditCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранного клиента из DataGrid
            var selectedCustomer = (Customer)CustomersDataGrid.SelectedItem;

            // Проверяем, что клиент выбран
            if (selectedCustomer != null)
            {
                try
                {
                    // Открываем окно редактирования с выбранным клиентом
                    var window = new CustomerEditWindow(selectedCustomer);
                    var result = window.ShowDialog(); // Показываем окно как диалог

                    // Если окно закрыто с результатом "ОК" (DialogResult == true), обновляем данные
                    if (result == true)
                    {
                        // Обновляем данные в DataGrid
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    // В случае ошибки выводим сообщение
                    MessageBox.Show($"Произошла ошибка при редактировании клиента: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                // Если клиент не выбран, выводим сообщение об ошибке
                MessageBox.Show("Пожалуйста, выберите клиента для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Обработчик для кнопки "Удалить"
        private void DeleteCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedCustomer = (Customer)CustomersDataGrid.SelectedItem;
            if (selectedCustomer != null)
            {
                var result = MessageBox.Show("Вы уверены, что хотите удалить этого клиента?", "Подтверждение", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _context.Customers.Remove(selectedCustomer);
                    _context.SaveChanges();
                    LoadData(); // Обновляем данные после удаления
                }
            }
            else
            {
                MessageBox.Show("Выберите клиента для удаления");
            }
        }
        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new OrderEditWindow();  // Создаем новое окно для добавления заказа
            var result = window.ShowDialog();  // Показываем окно как диалог

            if (result == true)
            {
                LoadOrdersData();  // Обновляем данные после добавления нового заказа
            }
        }

        private void LoadOrdersData()
        {
            using (var context = new SnackStoreContext())
            {
                OrdersDataGrid.ItemsSource = context.Orders.ToList();  // Заполняем DataGrid заказами
            }
        }
        private void EditOrderButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrder = (Order)OrdersDataGrid.SelectedItem;
            if (selectedOrder != null)
            {
                var window = new OrderEditWindow(selectedOrder); // Передаем заказ для редактирования
                var result = window.ShowDialog(); // Показываем окно как диалог
                if (result == true)
                {
                    LoadOrdersData(); // Загружаем обновленные данные заказов
                }
            }
            else
            {
                MessageBox.Show("Выберите заказ для редактирования"); // Сообщение, если заказ не выбран
            }
        }
        private void DeleteOrderButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrder = (Order)OrdersDataGrid.SelectedItem;
            if (selectedOrder != null)
            {
                var result = MessageBox.Show("Вы уверены, что хотите удалить этот заказ?", "Подтверждение", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        var context = new SnackStoreContext();
                        var orderToDelete = context.Orders.FirstOrDefault(o => o.OrderId == selectedOrder.OrderId);
                        if (orderToDelete != null)
                        {
                            context.Orders.Remove(orderToDelete);
                            context.SaveChanges(); // Сохраняем изменения в базе данных
                            LoadOrdersData(); // Обновляем данные в DataGrid
                        }
                        else
                        {
                            MessageBox.Show("Заказ не найден в базе данных");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите заказ для удаления"); // Сообщение, если заказ не выбран
            }
        }
        private void AddOrderItemButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new OrderItemEditWindow(); // Открытие окна для добавления новой позиции
            var result = window.ShowDialog(); // Показываем окно как диалог
            if (result == true)
            {
                LoadOrderItemsData(); // Загружаем обновленные данные
            }
        }

        private void EditOrderItemButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrderItem = (OrderItem)OrderItemsDataGrid.SelectedItem;
            if (selectedOrderItem != null)
            {
                var window = new OrderItemEditWindow(selectedOrderItem); // Передаем объект для редактирования
                var result = window.ShowDialog(); // Показываем окно как диалог
                if (result == true)
                {
                    LoadOrderItemsData(); // Загружаем обновленные данные
                }
            }
            else
            {
                MessageBox.Show("Выберите позицию заказа для редактирования");
            }
        }

        private void DeleteOrderItemButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrderItem = (OrderItem)OrderItemsDataGrid.SelectedItem;
            if (selectedOrderItem != null)
            {
                var result = MessageBox.Show("Вы уверены, что хотите удалить эту позицию заказа?", "Подтверждение", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        var context = new SnackStoreContext();
                        var orderItemToDelete = context.OrderItems.FirstOrDefault(oi => oi.OrderItemId == selectedOrderItem.OrderItemId);
                        if (orderItemToDelete != null)
                        {
                            context.OrderItems.Remove(orderItemToDelete);
                            context.SaveChanges();
                            LoadOrderItemsData(); // Обновляем данные в DataGrid
                        }
                        else
                        {
                            MessageBox.Show("Позиция заказа не найдена");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите позицию заказа для удаления");
            }

        }
        private void LoadOrderItemsData()
        {
            var context = new SnackStoreContext();
            OrderItemsDataGrid.ItemsSource = context.OrderItems.Include(oi => oi.Order).Include(oi => oi.Snack).ToList();
        }
        // Кнопка "Добавить"
        private void AddSnackButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new SnackEditWindow(); // Создаем новое окно для добавления
            var result = window.ShowDialog(); // Показываем окно как диалог
            if (result == true)
            {
                LoadData(); // Обновляем данные после добавления
            }
        }

        // Кнопка "Изменить"
        private void EditSnackButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedSnack = (Snack)SnacksDataGrid.SelectedItem;
            if (selectedSnack != null)
            {
                var window = new SnackEditWindow(selectedSnack); // Создаем окно для редактирования
                var result = window.ShowDialog(); // Показываем окно как диалог
                if (result == true)
                {
                    LoadData(); // Обновляем данные после редактирования
                }
            }
            else
            {
                MessageBox.Show("Выберите снек для редактирования.");
            }
        }

        // Кнопка "Удалить"
        private void DeleteSnackButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedSnack = (Snack)SnacksDataGrid.SelectedItem;
            if (selectedSnack != null)
            {
                var result = MessageBox.Show("Вы уверены, что хотите удалить этот снек?", "Подтверждение", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    var context = new SnackStoreContext();
                    context.Snacks.Remove(selectedSnack); // Удаляем выбранный снек
                    context.SaveChanges(); // Сохраняем изменения
                    LoadData(); // Обновляем данные в DataGrid
                }
            }
            else
            {
                MessageBox.Show("Выберите снек для удаления.");
            }
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var searchText = SearchTextBox.Text.ToLower(); // Получаем текст для поиска

            // Фильтруем список клиентов
            var filteredCustomers = _context.Customers
                .Where(c =>
                    c.FullName.ToLower().Contains(searchText) || // Поиск по полному имени (FirstName + LastName)
                    c.Email.ToLower().Contains(searchText)) // Поиск по email
                .ToList();

            // Обновляем источник данных для DataGrid
            CustomersDataGrid.ItemsSource = filteredCustomers;
        }

    }
}