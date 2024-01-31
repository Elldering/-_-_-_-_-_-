using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Windows;
using Информационная_Система_Учета_Товаров_на_Складе.uchot_tovarovDataSetTableAdapters;

namespace Информационная_Система_Учета_Товаров_на_Складе
{
    internal class DataBase
    {
        

    }
    public class Autorization_interface 
    {

        public bool AuthenticateUser(string login, string password)
        {
            try
            {
                EmployeeTableAdapter Authorizations = new EmployeeTableAdapter();
                var user = Authorizations.GetData().FirstOrDefault(a => a.Login == login && a.Password == password);

                if (user != null)
                {
                    if (user.Name_of_the_role == "administrator") 
                    {
                        OpenAdminInterface();
                    }
                    else if (user.Name_of_the_role == "employeer") 
                    {
                        OpenUserInterface();
                    }

                    return true; 
                }
                else
                {
                    MessageBox.Show("Неверные логин или пароль.", "Ошибка аутентификации", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка аутентификации: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false; 
            }
        }
        private product productWindow;
        private Autorization auturiz_window;
        private Employee_security employeeSecurityWindow;
        private Sklad skladWindow;
        private The_Admin_Panel_window adminPanelWindow;
        private void OpenAdminInterface()
        {
            if (adminPanelWindow == null || !adminPanelWindow.IsVisible)
            {
                adminPanelWindow = new The_Admin_Panel_window();
                adminPanelWindow.Show();
            }
            
        }

        private void OpenUserInterface()
        {
            if (productWindow == null || !productWindow.IsVisible)
            {
                productWindow = new product();
                productWindow.Show();
            }
        }
    }
}
