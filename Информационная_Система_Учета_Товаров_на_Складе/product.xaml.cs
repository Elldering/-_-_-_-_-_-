using System;
using System.Collections.Generic;
using System.Data;
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
using Информационная_Система_Учета_Товаров_на_Складе.uchot_tovarovDataSetTableAdapters;

namespace Информационная_Система_Учета_Товаров_на_Складе
{
    /// <summary>
    /// Логика взаимодействия для product.xaml
    /// </summary>
    public partial class product : Window
    {
        private Autorization auturiz_window;
        public product()
        {
            InitializeComponent();
            refresh();
        }
        private List<string> lastChanges = new List<string>();
        public void refresh()
        {
            ProductTableAdapter productData = new ProductTableAdapter();
            product_grid.ItemsSource = productData.GetData();
            name_product_tbx.Text = string.Empty;
            price_product_tbx.Text = string.Empty;
            count_product_tbx.Text = string.Empty;
            kategory_product_tbx.Text = string.Empty;
            name_product_tbx.Focus();
        }
        private void Exit1_Click(object sender, RoutedEventArgs e)
        {
            if (auturiz_window == null || !auturiz_window.IsVisible)
            {
                auturiz_window = new Autorization();
                auturiz_window.Show();
            }
            Close();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            ProductTableAdapter productData = new ProductTableAdapter();
            if (int.TryParse(count_product_tbx.Text, out int result) && int.TryParse(price_product_tbx.Text, out int res))
            {
                productData.InsertQuery(name_product_tbx.Text, Convert.ToInt32(count_product_tbx.Text), Convert.ToInt32(price_product_tbx.Text), kategory_product_tbx.Text);
                lastChanges.Add($"Добавлен продукт: {name_product_tbx.Text}");
                refresh();
            }
            else
            {
                MessageBox.Show("В полях количество товара и цена товара должны быть числовые значения");
            }
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)product_grid.SelectedItem;

            if (selectedRow != null)
            {
                int productId = Convert.ToInt32(selectedRow["ID_Product"]);

                ProductTableAdapter productAdapter = new ProductTableAdapter();

                try
                {
                    productAdapter.DeleteQuery(productId);
                    lastChanges.Add($"Удален продукт: {selectedRow["Product_name"]}");
                    refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении товара: {ex.Message}");
                }
            }
        }

        private void change_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)product_grid.SelectedItem;

            if (selectedRow != null)
            {
                int productId = Convert.ToInt32(selectedRow["ID_Product"]);
                string oldProductName = selectedRow["Product_name"].ToString();

                string newName = name_product_tbx.Text;
                int newCount = Convert.ToInt32(count_product_tbx.Text);
                int newPrice = Convert.ToInt32(price_product_tbx.Text);
                string newCategory = kategory_product_tbx.Text;

                ProductTableAdapter productAdapter = new ProductTableAdapter();

                try
                {
                    productAdapter.UpdateQuery(newName, newCount, newPrice, newCategory, productId);
                    lastChanges.Add($"Изменен продукт: {oldProductName} => {newName}");
                    refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при изменении товара: {ex.Message}");
                }
            }
        }

        private void product_grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)product_grid.SelectedItem;

            if (selectedRow != null)
            {
                int productId = Convert.ToInt32(selectedRow["ID_Product"]);
                string productName = selectedRow["Product_name"].ToString();
                int productCount = Convert.ToInt32(selectedRow["Number_of_products"]);
                int productPrice = Convert.ToInt32(selectedRow["Durability"]);
                string productCategory = selectedRow["Category"].ToString();

                // Заполняем поля данными из выбранной строки
                name_product_tbx.Text = productName;
                count_product_tbx.Text = productCount.ToString();
                price_product_tbx.Text = productPrice.ToString();
                kategory_product_tbx.Text = productCategory;
            }
        }

        private Sklad skladWindow;
        private void sklad_window_Click(object sender, RoutedEventArgs e)
        {
            if (skladWindow == null || !skladWindow.IsVisible)
            {
                skladWindow = new Sklad();
                skladWindow.Show();
            }
            Close();
        }

        private void changes_last_Click(object sender, RoutedEventArgs e)
        {
            ShowLastChanges();
        }
        private void ShowLastChanges()
        {
            string message = "Последние изменения продуктов:\n\n";

            foreach (var change in lastChanges)
            {
                message += change + "\n";
            }

            MessageBox.Show(message, "Изменения продуктов", MessageBoxButton.OK, MessageBoxImage.Information);
        }


    }
}
