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
    /// Логика взаимодействия для StrPubHouse.xaml
    /// </summary>
    public partial class StrPubHouse : Page
    {
        private BookstoreEntities bookstore = new BookstoreEntities();
        public StrPubHouse()
        {
            InitializeComponent();
            phDgr.ItemsSource = bookstore.PublishingHouse.ToList();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            if (nameTbx.Text != string.Empty)
            {
                PublishingHouse ph = new PublishingHouse();
                ph.PHName = nameTbx.Text;
                bookstore.PublishingHouse.Add(ph);
                bookstore.SaveChanges();
                phDgr.ItemsSource = bookstore.PublishingHouse.ToList();
                nameTbx.Clear();
            }
            else { MessageBox.Show("Убедитесь, что все поля заполнены"); }
        }

        private void change_Click(object sender, RoutedEventArgs e)
        {
            if(phDgr.SelectedItem != null && nameTbx.Text != string.Empty)
            {
                var ph = phDgr.SelectedItem as PublishingHouse;
                ph.PHName = nameTbx.Text;
                bookstore.SaveChanges();
                phDgr.ItemsSource = bookstore.PublishingHouse.ToList();
                nameTbx.Clear();
            }
            else { MessageBox.Show("Убедитесь, что все поля заполнены"); }
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            if (phDgr.SelectedItem != null)
            {
                bookstore.PublishingHouse.Remove(phDgr.SelectedItem as PublishingHouse);
                bookstore.SaveChanges();
                phDgr.ItemsSource = bookstore.PublishingHouse.ToList();
                nameTbx.Clear();
            }
            else { MessageBox.Show("Убедитесь, что Вы выбрали объект для удаления"); }
        }

        private void phDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (phDgr.SelectedItem != null)
            {
                var ph = phDgr.SelectedItem as PublishingHouse;
                nameTbx.Text = ph.PHName;
                ph.PHName = nameTbx.Text;
            }
        }
    }
}
