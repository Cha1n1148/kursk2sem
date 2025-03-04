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

    public partial class SnackEditWindow : Window
    {
        private Snack _snack;
        public SnackEditWindow()
        {
            InitializeComponent();
            _snack = new Snack(); // Создаем новый объект для добавления
            DataContext = _snack;
        }
        public SnackEditWindow(Snack snack)
        {
            InitializeComponent();
            _snack = snack; // Передаем существующий объект для редактирования
            DataContext = _snack;

            // Заполняем поля
            NameTextBox.Text = _snack.Name;
            CategoryTextBox.Text = _snack.Category;
            DescriptionTextBox.Text = _snack.Description;
            PriceTextBox.Text = _snack.Price.ToString("F2");
            QuantityTextBox.Text = _snack.Quantity.ToString();
        }
        // Обработчик для кнопки "Сохранить"
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, что все поля заполнены
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                string.IsNullOrWhiteSpace(CategoryTextBox.Text) ||
                string.IsNullOrWhiteSpace(DescriptionTextBox.Text) ||
                string.IsNullOrWhiteSpace(PriceTextBox.Text) ||
                string.IsNullOrWhiteSpace(QuantityTextBox.Text))
            {
                MessageBox.Show("Все поля обязательны для заполнения!");
                return;
            }

            // Заполняем данные объекта _snack
            _snack.Name = NameTextBox.Text;
            _snack.Category = CategoryTextBox.Text;
            _snack.Description = DescriptionTextBox.Text;
            _snack.Price = decimal.Parse(PriceTextBox.Text);
            _snack.Quantity = int.Parse(QuantityTextBox.Text);

            // Создаем контекст базы данных
            var context = new SnackStoreContext();

            try
            {
                if (_snack.SnackId == 0) // Если это новый снек, добавляем его
                {
                    context.Snacks.Add(_snack);
                }
                else // Если редактируем существующий снек
                {
                    var existingSnack = context.Snacks.FirstOrDefault(s => s.SnackId == _snack.SnackId);
                    if (existingSnack != null)
                    {
                        existingSnack.Name = _snack.Name;
                        existingSnack.Category = _snack.Category;
                        existingSnack.Description = _snack.Description;
                        existingSnack.Price = _snack.Price;
                        existingSnack.Quantity = _snack.Quantity;

                        context.Snacks.Update(existingSnack);
                    }
                }

                // Сохраняем изменения в базе данных
                context.SaveChanges();
                DialogResult = true; // Закрываем окно с успешным результатом
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        // Обработчик для кнопки "Отменить"
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // Закрываем окно без изменений
        }
    }
}
