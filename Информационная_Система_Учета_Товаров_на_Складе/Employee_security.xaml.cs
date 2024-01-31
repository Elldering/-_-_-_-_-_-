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
    /// Логика взаимодействия для Employee_security.xaml
    /// </summary>
    public partial class Employee_security : Window
    {
        private int originalIdAuthorization;
        private product productWindow;
        private Autorization auturiz_window;
        private Employee_security employeeSecurityWindow;
        private Sklad skladWindow;
        private The_Admin_Panel_window adminPanelWindow;

        public Employee_security()
        {
            
            AuthorizationUTableAdapter auth = new AuthorizationUTableAdapter();
            RoleTableAdapter role = new RoleTableAdapter();
            InitializeComponent();

            autorization_grid.ItemsSource = auth.GetData();
            Role_combobox.ItemsSource = role.GetData();
            Role_combobox.DisplayMemberPath = "Name_of_the_role";
            Role_combobox.SelectedValuePath = "ID_Role"; 
            refresh();
        }
        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранное значение из ComboBox
            int selectedRoleId = Convert.ToInt32(Role_combobox.SelectedValue);

            // Предполагаем, что у вас есть адаптер для таблицы Authorization
            AuthorizationUTableAdapter authAdapter = new AuthorizationUTableAdapter();

            try
            {
                // Выполняем вставку в таблицу
                authAdapter.InsertQuery(login_tbx.Text, password_tbx.Text, selectedRoleId);

                // Обновляем DataGrid
                autorization_grid.ItemsSource = authAdapter.GetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении записи: {ex.Message}");
            }
            

        }
        public void refresh()
        {
            // Предполагаем, что у вас есть адаптер для таблицы Authorization
            AuthorizationUTableAdapter authAdapter = new AuthorizationUTableAdapter();
            autorization_grid.ItemsSource = authAdapter.GetData();
            login_tbx.Text = string.Empty;
            password_tbx.Text = string.Empty;
            login_tbx.Focus();
        }
        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationUTableAdapter auth = new AuthorizationUTableAdapter();
            auth.DeleteQuery(originalIdAuthorization);
            refresh();
        }
        private void change_btn_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationUTableAdapter auth = new AuthorizationUTableAdapter();
            DataRowView selectedRow = (DataRowView)autorization_grid.SelectedItem;
            if (selectedRow != null)
            {
                originalIdAuthorization = Convert.ToInt32(selectedRow["ID_Authorization"]);
                auth.UpdateQuery(login_tbx.Text, password_tbx.Text, Convert.ToInt32(Role_combobox.SelectedValue), originalIdAuthorization);
            }
            
            refresh();
        }
        private void autorization_grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            DataRowView selectedRow = (DataRowView)autorization_grid.SelectedItem;

            if (selectedRow != null)
            {
                originalIdAuthorization = Convert.ToInt32(selectedRow["ID_Authorization"]);
                string originalLogin = selectedRow["Login"].ToString();
                login_tbx.Text = originalLogin;
                string originalPassword = selectedRow["Password"].ToString();
                password_tbx.Text = originalPassword;
                int originalIdRole = Convert.ToInt32(selectedRow["ID_Role"]);
                // Получение ID роли из выбранной строки
                int roleId = Convert.ToInt32(selectedRow["ID_Role"]);
                if (autorization_grid.SelectedItem is DataRowView selectedRowInView)
                {
                    // Установка выбранной строки в ComboBox
                    Role_combobox.SelectedItem = selectedRowInView.Row;
                }
                // Установка выбранной роли в ComboBox
                Role_combobox.SelectedValue = roleId;
            }

        }
        private void registrate_employee_Click(object sender, RoutedEventArgs e)
        {
            if (adminPanelWindow == null || !adminPanelWindow.IsVisible)
            {
                adminPanelWindow = new The_Admin_Panel_window();
                adminPanelWindow.Show();
            }
            Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            if (auturiz_window == null || !auturiz_window.IsVisible)
            {
                auturiz_window = new Autorization();
                auturiz_window.Show();
            }
            Close();
        }
    }
}
