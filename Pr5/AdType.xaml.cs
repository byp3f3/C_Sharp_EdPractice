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
    /// Логика взаимодействия для AdType.xaml
    /// </summary>
    public partial class AdType : Page
    {
        private BookstoreEntities bookstore = new BookstoreEntities();
        public AdType()
        {
            InitializeComponent();
            typeDgr.ItemsSource = bookstore.WorkplaceType.ToList();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            if(typeTbx.Text != string.Empty)
            {
                if (Check(typeTbx.Text) == true)
                {
                    WorkplaceType workplace = new WorkplaceType();
                    workplace.TypeName = typeTbx.Text;
                    bookstore.WorkplaceType.Add(workplace);
                    bookstore.SaveChanges();
                    typeDgr.ItemsSource = bookstore.WorkplaceType.ToList();
                    typeTbx.Clear();
                }
                else { MessageBox.Show("Роль может содержать только буквы"); }
            }
            else { MessageBox.Show("Убедитесь, что все поля заполнены"); }
        }

        private void change_Click(object sender, RoutedEventArgs e)
        {
            if(typeDgr.SelectedItem != null && typeTbx.Text != string.Empty)
            {
                if (Check(typeTbx.Text) == true)
                {
                    var workplace = (WorkplaceType)typeDgr.SelectedItem;
                    workplace.TypeName = typeTbx.Text;
                    bookstore.SaveChanges();
                    typeDgr.ItemsSource = bookstore.WorkplaceType.ToList();
                    typeTbx.Clear();
                }
                else { MessageBox.Show("Роль может содержать только буквы"); }
            }
            else { MessageBox.Show("Убедитесь, что все поля заполнены"); }
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            if(typeDgr.SelectedItem != null)
            {
                bookstore.WorkplaceType.Remove(typeDgr.SelectedItem as WorkplaceType);
                bookstore.SaveChanges();
                typeDgr.ItemsSource = bookstore.WorkplaceType.ToList();
                typeTbx.Clear();
            }
            else { MessageBox.Show("Убедитесь, что Вы выбрали объект для удаления"); }
        }

        private void typeDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (typeDgr.SelectedItem != null)
            {
                var workplace = (WorkplaceType)typeDgr.SelectedItem;
                typeTbx.Text = workplace.TypeName;
                workplace.TypeName = typeTbx.Text;
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
