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
    /// Логика взаимодействия для The_Admin_Panel_window.xaml
    /// </summary>
    public partial class The_Admin_Panel_window : Window
    {
        private Autorization auturiz_window;

        public The_Admin_Panel_window()
        {
            InitializeComponent();
            refresh();
        }

        private Employee_security employeeSecurityWindow;

        private void employee_security_window_Click(object sender, RoutedEventArgs e)
        {
            if (employeeSecurityWindow == null || !employeeSecurityWindow.IsVisible)
            {
                employeeSecurityWindow = new Employee_security();
                employeeSecurityWindow.Show();
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

        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            EmployeeTableAdapter emp = new EmployeeTableAdapter();

            DataRowView selectedRow = (DataRowView)employee_grid.SelectedItem;
            if (selectedRow != null)
            {
                int employeeId = Convert.ToInt32(selectedRow["ID_Employee"]);
                emp.DeleteQuery(employeeId);
                refresh();
            }
        }

        private void change_btn_Click(object sender, RoutedEventArgs e)
        {
            EmployeeTableAdapter emp = new EmployeeTableAdapter();

            DataRowView selectedRow = (DataRowView)employee_grid.SelectedItem;
            if (selectedRow != null)
            {
                int employeeId = Convert.ToInt32(selectedRow["ID_Employee"]);
                emp.UpdateQuery(surname_tbx.Text, name_tbx.Text, Convert.ToInt32(oklad_tbx.Text), Convert.ToInt32(age_tbx.Text), Convert.ToInt32(Login_combobox.SelectedValue), employeeId);
                refresh();
            }
        }

        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            EmployeeTableAdapter emp = new EmployeeTableAdapter();

            if (int.TryParse(oklad_tbx.Text, out int oklad) && int.TryParse(age_tbx.Text, out int age))
            {
                emp.InsertQuery(surname_tbx.Text, name_tbx.Text, oklad, age, Convert.ToInt32(Login_combobox.SelectedValue));
                refresh();
            }
            else
            {
                MessageBox.Show("Поля оклада и возраста должны быть числовыми значениями.");
            }
        }
        private void employee_grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)employee_grid.SelectedItem;

            if (selectedRow != null)
            {
                int employeeId = Convert.ToInt32(selectedRow["ID_Employee"]);
                string name = selectedRow["name"].ToString();
                string surname = selectedRow["Surname"].ToString();
                int employeeOklad = Convert.ToInt32(selectedRow["Salary"]);
                int employeeAge = Convert.ToInt32(selectedRow["Age"]);
                int employeeLoginId = Convert.ToInt32(selectedRow["ID_Authorization"]);
                string login = selectedRow["name"].ToString();

                // Заполняем поля данными из выбранной строки
                name_tbx.Text = name;
                surname_tbx.Text = surname;
                oklad_tbx.Text = employeeOklad.ToString();
                age_tbx.Text = employeeAge.ToString();

                EmployeeTableAdapter author = new EmployeeTableAdapter();

                DataRowView selectedRoleRow = (DataRowView)Login_combobox.SelectedItem;

                if (selectedRoleRow != null)
                {

                    Login_combobox.SelectedItem = selectedRoleRow.Row;
                }
                // Установка выбранной роли в ComboBox
                Login_combobox.SelectedValue = employeeLoginId;

            }
        }

        public void refresh()
        {
            EmployeeTableAdapter emp = new EmployeeTableAdapter();
            employee_grid.ItemsSource = emp.GetData();
            surname_tbx.Text = string.Empty;
            name_tbx.Text = string.Empty;
            oklad_tbx.Text = string.Empty;
            age_tbx.Text = string.Empty;
            Login_combobox.Text = string.Empty;
            AuthorizationUTableAdapter auth = new AuthorizationUTableAdapter();
            Login_combobox.ItemsSource = auth.GetData();
            Login_combobox.DisplayMemberPath = "Login";
            Login_combobox.SelectedValuePath = "ID_Authorization";
            surname_tbx.Focus();
        }
    }
}
