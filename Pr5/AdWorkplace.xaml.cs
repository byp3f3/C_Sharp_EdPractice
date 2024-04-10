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
    /// Логика взаимодействия для AdWorkplace.xaml
    /// </summary>
    public partial class AdWorkplace : Page
    {
        private BookstoreEntities bookstore = new BookstoreEntities();
        public AdWorkplace()
        {
            InitializeComponent();
            workplaceDgr.ItemsSource = bookstore.Workplace.ToList();
            cityCbx.ItemsSource = bookstore.City.ToList();
            cityCbx.DisplayMemberPath = "CityName";
            cityCbx.SelectedValuePath = "ID_City";
            typeCbx.ItemsSource = bookstore.WorkplaceType.ToList();
            typeCbx.DisplayMemberPath = "TypeName";
            typeCbx.SelectedValuePath = "ID_WType";
            filter.ItemsSource = bookstore.City.ToList();
            filter.DisplayMemberPath = "CityName";
            filter.SelectedValuePath = "ID_City";
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            if(nameTbx.Text != string.Empty && cityCbx.SelectedItem != null && typeCbx.SelectedItem !=null && streetTbx.Text != string.Empty && houseTbx.Text != string.Empty && buildTbx.Text != string.Empty)
            {
                if (Check(streetTbx.Text) == true)
                {
                    int amount;
                    if (int.TryParse(houseTbx.Text, out amount) && amount > 0)
                    {
                        int cost;
                        if (int.TryParse(buildTbx.Text, out cost) && cost > 0)
                        {
                            Workplace work = new Workplace();
                            work.WorkName = nameTbx.Text;
                            work.City_ID = (int)cityCbx.SelectedValue;
                            work.WType_ID = (int)typeCbx.SelectedValue;
                            work.Street = streetTbx.Text;
                            work.House = Convert.ToInt32(houseTbx.Text);
                            work.Building = Convert.ToInt32(buildTbx.Text);
                            bookstore.Workplace.Add(work);
                            bookstore.SaveChanges();
                            workplaceDgr.ItemsSource = bookstore.Workplace.ToList();
                            nameTbx.Clear();
                            streetTbx.Clear();
                            houseTbx.Clear();
                            buildTbx.Clear();
                            cityCbx.SelectedItem = null;
                            typeCbx.SelectedItem = null;
                        }
                        else MessageBox.Show("Номер корпуса должен быть положительным числом");
                    }
                    else MessageBox.Show("Номер дома должен быть положительным числом");
                }
                else MessageBox.Show("Название улицы может содержать только буквы и цифры");
            }
            else { MessageBox.Show("Убедитесь, что все поля заполнены"); }
        }

        private void change_Click(object sender, RoutedEventArgs e)
        {
            if(workplaceDgr.SelectedItem != null && nameTbx.Text != string.Empty && cityCbx.SelectedItem != null && typeCbx.SelectedItem != null && streetTbx.Text != string.Empty && houseTbx.Text != string.Empty && buildTbx.Text != string.Empty)
            {
                if(Check(streetTbx.Text) == true)
                {
                    int amount;
                    if (int.TryParse(houseTbx.Text, out amount) && amount > 0)
                    {
                        int cost;
                        if (int.TryParse(buildTbx.Text, out cost) && cost > 0)
                        {
                            var work = workplaceDgr.SelectedItem as Workplace;
                            work.WorkName = nameTbx.Text;
                            work.City_ID = (int)cityCbx.SelectedValue;
                            work.WType_ID = (int)typeCbx.SelectedValue;
                            work.Street = streetTbx.Text;
                            work.House = Convert.ToInt32(houseTbx.Text);
                            work.Building = Convert.ToInt32(buildTbx.Text);
                            bookstore.SaveChanges();
                            workplaceDgr.ItemsSource = bookstore.Workplace.ToList();
                            nameTbx.Clear();
                            streetTbx.Clear();
                            houseTbx.Clear();
                            buildTbx.Clear();
                            cityCbx.SelectedItem = null;
                            typeCbx.SelectedItem = null;
                        }
                        else MessageBox.Show("Номер корпуса должен быть положительным числом");
                    }
                    else MessageBox.Show("Номер дома должен быть положительным числом");
                }
                else MessageBox.Show("Название улицы может содержать только буквы и цифры");
            }
            else { MessageBox.Show("Убедитесь, что все поля заполнены"); }
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            if (workplaceDgr.SelectedItem != null)
            {
                bookstore.Workplace.Remove(workplaceDgr.SelectedItem as Workplace);
                bookstore.SaveChanges();
                workplaceDgr.ItemsSource = bookstore.Workplace.ToList();
                nameTbx.Clear();
                streetTbx.Clear();
                houseTbx.Clear();
                buildTbx.Clear();
                cityCbx.SelectedItem = null;
                typeCbx.SelectedItem = null;
            }
            else { MessageBox.Show("Убедитесь, что Вы выбрали объект для удаления"); }
        }

        private void worplaceDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (workplaceDgr.SelectedItem != null)
            {
                var work = workplaceDgr.SelectedItem as Workplace;
                nameTbx.Text = work.WorkName;
                cityCbx.SelectedValue = work.City_ID;
                typeCbx.SelectedValue = work.WType_ID;
                streetTbx.Text = work.Street;
                houseTbx.Text = work.House.ToString();
                buildTbx.Text = work.Building.ToString();
                work.WorkName = nameTbx.Text;
                work.City_ID = (int)cityCbx.SelectedValue;
                work.WType_ID = (int)typeCbx.SelectedValue;
                work.Street = streetTbx.Text;
                work.House = Convert.ToInt32(houseTbx.Text);
                work.Building = Convert.ToInt32(buildTbx.Text);
            }
        }

        private void filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (filter.SelectedItem != null)
            {
                var selected = filter.SelectedItem as City;
                workplaceDgr.ItemsSource = bookstore.Workplace.ToList().Where(item => item.City_ID == selected.ID_City);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            workplaceDgr.ItemsSource = bookstore.Workplace.ToList();
            filter.SelectedItem = null;
        }
        bool Check(string line)
        {
            foreach (char c in line)
            {
                if (Char.IsLetter(c) || Char.IsDigit(c))
                {
                    continue;
                }
                else return false;
            }
            return true;
        }
    }
}
