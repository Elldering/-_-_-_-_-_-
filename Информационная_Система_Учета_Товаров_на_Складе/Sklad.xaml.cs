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
    /// Логика взаимодействия для Sklad.xaml
    /// </summary>
    public partial class Sklad : Window
    {
        private product productWindow;
        private Employee_security employeeSecurityWindow;
        private Sklad skladWindow;
        private The_Admin_Panel_window adminPanelWindow;
        public Sklad()
        {
            InitializeComponent();
            WarehouseTableAdapter skladData = new WarehouseTableAdapter();
            InitializeComponent();

            sklad_grid.ItemsSource = skladData.GetData();

            EmployeeTableAdapter employeeTableData = new EmployeeTableAdapter();
            Name_combobox.ItemsSource = employeeTableData.GetData();
            Name_combobox.DisplayMemberPath = "Name";
            Name_combobox.SelectedValuePath = "ID_Employee";
            ProductTableAdapter productTableData = new ProductTableAdapter();
            name_product_combobox.ItemsSource = productTableData.GetData();
            name_product_combobox.DisplayMemberPath = "Product_name";
            name_product_combobox.SelectedValuePath = "ID_Product";
        }
        private Autorization auturiz_window;


        private void Product_window_Click(object sender, RoutedEventArgs e)
        {
            if (productWindow == null || !productWindow.IsVisible)
            {
                productWindow = new product();
                productWindow.Show();
            }
            Close();
        }

        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранные значения из ComboBox
            int employeeId = Convert.ToInt32(Name_combobox.SelectedValue);
            int productId = Convert.ToInt32(name_product_combobox.SelectedValue);

            // Предполагаем, что у вас есть адаптер для таблицы Sklad
            WarehouseTableAdapter skladAdapter = new WarehouseTableAdapter();

            try
            {
                // Выполняем вставку в таблицу
                skladAdapter.InsertQuery(productId, employeeId, address_sklad.Text);

                // Обновляем DataGrid
                sklad_grid.ItemsSource = skladAdapter.GetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении записи: {ex.Message}");
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            if (auturiz_window == null || !auturiz_window.IsVisible)
            {
                auturiz_window = new Autorization();
                auturiz_window.Show();
            }
            Close();
        }

        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            WarehouseTableAdapter skladAdapter = new WarehouseTableAdapter();
            DataRowView selectedRow = (DataRowView)sklad_grid.SelectedItem;
            if (selectedRow != null)
            {
                int warehouseId = Convert.ToInt32(selectedRow["ID_Warehouse"]);
                skladAdapter.DeleteQuery(warehouseId);
            }
            refresh();
        }

        public void refresh()
        {
            WarehouseTableAdapter skladAdapter = new WarehouseTableAdapter();
            EmployeeTableAdapter employeeTableData = new EmployeeTableAdapter();
            sklad_grid.ItemsSource= skladAdapter.GetData();
            Name_combobox.ItemsSource = employeeTableData.GetData();
            EmployeeTableAdapter emp = new EmployeeTableAdapter();
            address_sklad.Text = string.Empty;
        }

        private void sklad_grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)sklad_grid.SelectedItem;

            if (selectedRow != null)
            {
                int employeeId = Convert.ToInt32(selectedRow["ID_Employee"]);
                int productId = Convert.ToInt32(selectedRow["ID_Product"]);

                // Устанавливаем выбранные значения в ComboBox
                Name_combobox.SelectedValue = employeeId;
                name_product_combobox.SelectedValue = productId;

                string address = selectedRow["Warehouse_address"].ToString();
                address_sklad.Text = address;
            }
        }

        private void cahnge_btn_Click(object sender, RoutedEventArgs e)
        {

            WarehouseTableAdapter skladAdapter = new WarehouseTableAdapter();
            DataRowView selectedRow = (DataRowView)sklad_grid.SelectedItem;
            if (selectedRow != null)
            {
                int warehouseId = Convert.ToInt32(selectedRow["ID_Warehouse"]);
                int employeeId = Convert.ToInt32(Name_combobox.SelectedValue);
                int productId = Convert.ToInt32(name_product_combobox.SelectedValue);

                // Выполняем обновление записи в таблице
                skladAdapter.UpdateQuery( productId, employeeId, address_sklad.Text, warehouseId);
            }
            refresh();
        }
    }
}
