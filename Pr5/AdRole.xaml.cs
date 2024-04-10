using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;


namespace Pr5
{
    /// <summary>
    /// Логика взаимодействия для AdRole.xaml
    /// </summary>
    public partial class AdRole : Page
    {
        private  BookstoreEntities bookstore = new BookstoreEntities();
        public AdRole()
        {
            InitializeComponent();
            roleDgr.ItemsSource = bookstore.UserRole.ToList();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            
            if (roleTbx.Text != string.Empty)
            {
                if (Check(roleTbx.Text) == true)
                {
                    UserRole role = new UserRole();
                    role.RoleName = roleTbx.Text;
                    bookstore.UserRole.Add(role);
                    bookstore.SaveChanges();
                    roleDgr.ItemsSource = bookstore.UserRole.ToList();
                    roleTbx.Clear();
                }
                else { MessageBox.Show("Роль может содержать только буквы"); }
            }
            else { MessageBox.Show("Убедитесь, что все поля заполнены"); }
        }

        private void change_Click(object sender, RoutedEventArgs e)
        {
            if (roleDgr.SelectedItem != null && roleTbx.Text != string.Empty )
            {
                if(Check(roleTbx.Text) == true)
                {
                    var role = (UserRole)roleDgr.SelectedItem;
                    role.RoleName = roleTbx.Text;
                    bookstore.SaveChanges();
                    roleDgr.ItemsSource = bookstore.UserRole.ToList();
                    roleTbx.Clear();
                }
                else { MessageBox.Show("Роль может содержать только буквы"); }
            }
            else { MessageBox.Show("Убедитесь, что все поля заполнены"); }
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            if (roleDgr.SelectedItem != null)
            {
                bookstore.UserRole.Remove(roleDgr.SelectedItem as  UserRole);
                bookstore.SaveChanges();
                roleDgr.ItemsSource = bookstore.UserRole.ToList();
                roleTbx.Clear();
            }
            else { MessageBox.Show("Убедитесь, что Вы выбрали объект для удаления"); }

        }

        private void roleDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (roleDgr.SelectedItem != null)
            {
                var role = (UserRole)roleDgr.SelectedItem;
                roleTbx.Text = role.RoleName;
                role.RoleName = roleTbx.Text;
            }
        }

        public static T DeserializeObject<T>()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                string json = File.ReadAllText(dialog.FileName);
                T obj = JsonConvert.DeserializeObject<T>(json);
                return obj;
            }
            else
            {
                return default(T);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<UserRole> roles = DeserializeObject<List<UserRole>>();
            foreach (UserRole role in roles)
            {
                role.RoleName = role.RoleName;
                bookstore.UserRole.Add(role);
                bookstore.SaveChanges();
                roleDgr.ItemsSource = bookstore.UserRole.ToList();
            }
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
