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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pr5
{
    /// <summary>
    /// Логика взаимодействия для AdUsers.xaml
    /// </summary>
    public partial class AdUsers : Page
    {
        private BookstoreEntities bookstore = new BookstoreEntities();
        public AdUsers()
        {
            InitializeComponent();
            usersDgr.ItemsSource = bookstore.Users.ToList();
            workplaceCbx.ItemsSource = bookstore.Workplace.ToList();
            workplaceCbx.DisplayMemberPath = "WorkName";
            workplaceCbx.SelectedValuePath = "ID_Workplace";
            roleCbx.ItemsSource = bookstore.UserRole.ToList();
            roleCbx.SelectedValuePath = "ID_Role";
            filter.ItemsSource = bookstore.UserRole.ToList();
            filter.DisplayMemberPath = "RoleName";
            filter.SelectedValuePath = "ID_Role";
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            if(surnameTbx.Text != string.Empty && nameTbx.Text != string.Empty && roleCbx.SelectedItem != null && workplaceCbx.SelectedItem != null)
            {
                if (Check(surnameTbx.Text) == true && Check(nameTbx.Text) == true && Check(midnameTbx.Text) == true)
                {
                    Users user = new Users();
                    user.Surname = surnameTbx.Text;
                    user.FirstName = nameTbx.Text;
                    user.MiddleName = midnameTbx.Text;
                    user.Role_ID = (int)roleCbx.SelectedValue;
                    user.Workplace_ID = (int)workplaceCbx.SelectedValue;
                    bookstore.Users.Add(user);
                    bookstore.SaveChanges();
                    usersDgr.ItemsSource = bookstore.Users.ToList();
                    surnameTbx.Clear();
                    nameTbx.Clear();
                    midnameTbx.Clear();
                    roleCbx.SelectedItem = null;
                    workplaceCbx.SelectedItem = null;
                }
                else { MessageBox.Show("ФИО может содержать только буквы"); }
            }
            else { MessageBox.Show("Убедитесь, что все поля заполнены"); }
        }

        private void change_Click(object sender, RoutedEventArgs e)
        {
            if(usersDgr.SelectedItem != null && surnameTbx.Text != string.Empty && nameTbx.Text != string.Empty && roleCbx.SelectedItem != null && workplaceCbx.SelectedItem != null)
            {
                if (Check(surnameTbx.Text) == true && Check(nameTbx.Text) == true && Check(midnameTbx.Text) == true)
                {
                    var user = (Users)usersDgr.SelectedItem;
                    user.Surname = surnameTbx.Text;
                    user.FirstName = nameTbx.Text;
                    user.MiddleName = midnameTbx.Text;
                    user.Role_ID = (int)roleCbx.SelectedValue;
                    user.Workplace_ID = (int)workplaceCbx.SelectedValue;
                    bookstore.SaveChanges();
                    usersDgr.ItemsSource = bookstore.Users.ToList();
                    surnameTbx.Clear();
                    nameTbx.Clear();
                    midnameTbx.Clear();
                    roleCbx.SelectedItem = null;
                    workplaceCbx.SelectedItem = null;
                }
                else { MessageBox.Show("ФИО может содержать только буквы"); }
            }
            else { MessageBox.Show("Убедитесь, что все поля заполнены"); }
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            if (usersDgr.SelectedItem != null)
            {
                bookstore.Users.Remove(usersDgr.SelectedItem as Users);
                bookstore.SaveChanges();
                usersDgr.ItemsSource = bookstore.Users.ToList();
                surnameTbx.Clear();
                nameTbx.Clear();
                midnameTbx.Clear();
                roleCbx.SelectedItem = null;
                workplaceCbx.SelectedItem = null;
            }
            else { MessageBox.Show("Убедитесь, что Вы выбрали объект для удаления"); }
        }

        private void usersDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (usersDgr.SelectedItem != null)
            {
                var user = (Users)usersDgr.SelectedItem;
                surnameTbx.Text = user.Surname;
                nameTbx.Text = user.FirstName;
                midnameTbx.Text = user.MiddleName;
                roleCbx.SelectedValue = user.Role_ID;
                workplaceCbx.SelectedValue = user.Workplace_ID;
                user.Surname = surnameTbx.Text;
                user.FirstName = nameTbx.Text;
                user.MiddleName = midnameTbx.Text;
                user.Role_ID = (int)roleCbx.SelectedValue;
                user.Workplace_ID = (int)workplaceCbx.SelectedValue;
            }
        }

        private void filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (filter.SelectedItem != null)
            {
                var selected = filter.SelectedItem as UserRole;
                usersDgr.ItemsSource = bookstore.Users.ToList().Where(item => item.Role_ID == selected.ID_Role);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            usersDgr.ItemsSource = bookstore.Users.ToList();
            filter.SelectedItem = null;
        }

        bool Check(string line)
        {
            foreach (char c in line)
            {
                if (Char.IsLetter(c))
                {
                    continue;
                }
                else return false;
            }
            return true;
        }
    }
}
