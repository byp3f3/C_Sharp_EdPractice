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
    /// Логика взаимодействия для AdCity.xaml
    /// </summary>
    public partial class AdCity : Page
    {
        private BookstoreEntities bookstore = new BookstoreEntities();
        public AdCity()
        {
            InitializeComponent();
            cityDgr.ItemsSource = bookstore.City.ToList();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            if(cityTbx.Text != string.Empty)
            {
                City city = new City();
                city.CityName = cityTbx.Text;
                bookstore.City.Add(city);
                bookstore.SaveChanges();
                cityDgr.ItemsSource = bookstore.City.ToList();
                cityTbx.Clear();
            }
            else { MessageBox.Show("Убедитесь, что все поля заполнены"); }
        }

        private void change_Click(object sender, RoutedEventArgs e)
        {
            if(cityDgr.SelectedItem != null && cityTbx.Text != string.Empty)
            {
                var city = cityDgr.SelectedItem as City;
                city.CityName = cityTbx.Text;
                bookstore.SaveChanges();
                cityDgr.ItemsSource = bookstore.City.ToList();
                cityTbx.Clear();
            }
            else { MessageBox.Show("Убедитесь, что все поля заполнены"); }
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            if (cityDgr.SelectedItem != null)
            {
                bookstore.City.Remove(cityDgr.SelectedItem as City);
                bookstore.SaveChanges();
                cityDgr.ItemsSource = bookstore.City.ToList();
                cityTbx.Clear();
            }
            else { MessageBox.Show("Убедитесь, что Вы выбрали объект для удаления"); }
        }

        private void cityDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cityDgr.SelectedItem != null)
            {
                var city = cityDgr.SelectedItem as City;
                cityTbx.Text = city.CityName;
                city.CityName = cityTbx.Text;
            }
            
        }
    }
}
