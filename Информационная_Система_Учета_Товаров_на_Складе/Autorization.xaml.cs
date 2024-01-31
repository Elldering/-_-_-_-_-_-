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
using Информационная_Система_Учета_Товаров_на_Складе.uchot_tovarovDataSetTableAdapters;

namespace Информационная_Система_Учета_Товаров_на_Складе
{
    /// <summary>
    /// Логика взаимодействия для Autorization.xaml
    /// </summary>
    public partial class Autorization : Window
    {
        public Autorization()
        {
            InitializeComponent();
            login_user.Focus();
            this.WindowState = WindowState.Maximized;
        }

        private void enter_butt_Click(object sender, RoutedEventArgs e)
        {
            string login = login_user.Text;
            string password = password_user.Password;

            Autorization_interface enter = new Autorization_interface();
            if (enter.AuthenticateUser(login, password))
            {
                Close();
            }
            
        }




        private product productWindow;
        private Employee_security employeeSecurityWindow;
        private Sklad skladWindow;
        private The_Admin_Panel_window adminPanelWindow;

        private void window1_Click(object sender, RoutedEventArgs e)
        {
            if (productWindow == null || !productWindow.IsVisible)
            {
                productWindow = new product();
                productWindow.Show();
            }
            Close();
        }

        private void window2_Click(object sender, RoutedEventArgs e)
        {
            if (employeeSecurityWindow == null || !employeeSecurityWindow.IsVisible)
            {
                employeeSecurityWindow = new Employee_security();
                employeeSecurityWindow.Show();
            }
            Close();
        }

        private void window3_Click(object sender, RoutedEventArgs e)
        {
            if (skladWindow == null || !skladWindow.IsVisible)
            {
                skladWindow = new Sklad();
                skladWindow.Show();
            }
            Close();
        }

        private void window4_Click(object sender, RoutedEventArgs e)
        {
            if (adminPanelWindow == null || !adminPanelWindow.IsVisible)
            {
                adminPanelWindow = new The_Admin_Panel_window();
                adminPanelWindow.Show();
            }
            Close();
        }

        private void password_user_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }
    }

}
