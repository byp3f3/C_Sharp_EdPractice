using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static MaterialDesignThemes.Wpf.Theme;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Pr5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BookstoreEntities bookstore = new BookstoreEntities();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var allloging = bookstore.LoginPassword.ToList();
            var users = bookstore.Users.ToList();

            for (int i = 0; i < allloging.Count; i++)
            {
                if (allloging[i].UserLogin == login.Text && allloging[i].UserPassword == password.Password)
                {
                    for(int j = 0; j < users.Count; j++)
                    {
                        if (allloging[i].Users_ID == users[j].ID_User)
                        {
                            int roleId = users[j].Role_ID;

                            switch (roleId)
                            {
                                case 1:
                                    AdminWindow adminWindow = new AdminWindow();
                                    adminWindow.Show();
                                    Close();
                                    break;
                                case 2:
                                    CashierWindow cashierWindow = new CashierWindow();
                                    cashierWindow.Show();
                                    Close();
                                    break;
                                case 3:
                                    StorageWindow storageWindow = new StorageWindow();
                                    storageWindow.Show();
                                    Close();
                                    break;
                                default:
                                    System.Windows.MessageBox.Show("У вас нет доступа:(");
                                    break;

                            }
                        }
                    }
                }    
            }  
        }
    }
}
