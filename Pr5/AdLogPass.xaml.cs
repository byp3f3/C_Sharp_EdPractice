using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pr5
{
    /// <summary>
    /// Логика взаимодействия для AdLogPass.xaml
    /// </summary>
    public partial class AdLogPass : Page
    {
        private BookstoreEntities bookstore= new BookstoreEntities();
        public AdLogPass()
        {
            InitializeComponent();
            logpasDgr.ItemsSource = bookstore.LoginPassword.ToList();
            userCbx.ItemsSource = bookstore.Users.ToList();
            userCbx.DisplayMemberPath = "Surname";
            userCbx.SelectedValuePath = "ID_User";
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            if(loginTbx.Text != string.Empty && passwordTbx.Password != string.Empty && userCbx.SelectedItem != null)
            {
                if(Check(loginTbx.Text)==true && Check(passwordTbx.Password) == true)
                {
                    if(passwordTbx.Password.Length > 8)
                    {
                        LoginPassword loginPassword = new LoginPassword();
                        loginPassword.UserLogin = loginTbx.Text;
                        loginPassword.UserPassword = passwordTbx.Password.ToString();
                        loginPassword.Users_ID = (int)userCbx.SelectedValue;
                        bookstore.LoginPassword.Add(loginPassword);
                        bookstore.SaveChanges();
                        logpasDgr.ItemsSource = bookstore.LoginPassword.ToList();
                        loginTbx.Clear();
                        passwordTbx.Clear();
                        userCbx.SelectedItem = null;
                    }
                    else MessageBox.Show("Пароль должен быть более 8 символов");
                }
                else MessageBox.Show("Для логина и пароля можно использовать только латиницу и цифры");
            }
            else { MessageBox.Show("Убедитесь, что все поля заполнены"); }
        }

        private void change_Click(object sender, RoutedEventArgs e)
        {
            if (logpasDgr.SelectedItem != null && loginTbx.Text != string.Empty && passwordTbx.Password != string.Empty && userCbx.SelectedItem != null)
            {
                if (Check(loginTbx.Text) == true && Check(passwordTbx.Password) == true)
                {
                    if (passwordTbx.Password.Length > 8)
                    {
                        var selected = (LoginPassword)logpasDgr.SelectedItem;
                        selected.UserLogin = loginTbx.Text;
                        selected.UserPassword = passwordTbx.Password.ToString();
                        selected.Users_ID = (int)userCbx.SelectedValue;
                        bookstore.SaveChanges();
                        logpasDgr.ItemsSource = bookstore.LoginPassword.ToList();
                        loginTbx.Clear();
                        passwordTbx.Clear();
                        userCbx.SelectedItem = null;
                    }
                    else MessageBox.Show("Пароль должен быть более 8 символов");
                }
                else MessageBox.Show("Для логина и пароля можно использовать только латиницу и цифры");
            }
            else { MessageBox.Show("Убедитесь, что все поля заполнены"); }
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            if(logpasDgr.SelectedItem != null)
            {
                bookstore.LoginPassword.Remove(logpasDgr.SelectedItem as LoginPassword);
                bookstore.SaveChanges();
                logpasDgr.ItemsSource = bookstore.LoginPassword.ToList();
                loginTbx.Clear();
                passwordTbx.Clear();
                userCbx.SelectedItem = null;
            }
            else { MessageBox.Show("Убедитесь, что Вы выбрали объект для удаления"); }
        }

        private void logpasDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (logpasDgr.SelectedItem != null)
            {
                var selected = (LoginPassword)logpasDgr.SelectedItem;
                loginTbx.Text = selected.UserLogin;
                passwordTbx.Password = selected.UserPassword;
                userCbx.SelectedValue = selected.Users_ID;
                selected.UserLogin = loginTbx.Text;
                selected.UserPassword = passwordTbx.Password.ToString();
                selected.Users_ID = (int)userCbx.SelectedValue;
                
            }
        }

        bool Check(string line)
        {
            foreach (char c in line)
            {
                if (Char.IsDigit(c) || (int)c >= 97 && (int)c <= 122)
                {
                    continue;
                }
                else return false;
            }
            return true;
        }
    }
}
