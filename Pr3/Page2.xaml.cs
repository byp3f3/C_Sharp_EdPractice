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

namespace Pr3
{
    /// <summary>
    /// Логика взаимодействия для Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        private BooksEntities books = new BooksEntities();
        public Page2()
        {
            InitializeComponent();
            bookDgr.ItemsSource = books.Book.ToList();
            SearchCbx.ItemsSource = books.Book.ToList();
        }

        private void SearchBut_Click(object sender, RoutedEventArgs e)
        {
            bookDgr.ItemsSource = books.Book.ToList().Where(item => item.Book_name.Contains(SearchTbx.Text));
        }

        private void ClearBut_Click(object sender, RoutedEventArgs e)
        {
            bookDgr.ItemsSource = books.Book.ToList();
            SearchCbx.SelectedItem = null;
            SearchTbx.Clear();
        }

        private void SearchCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SearchCbx.SelectedItem != null)
            {
                var selected = SearchCbx.SelectedItem as Book;
                bookDgr.ItemsSource = books.Book.ToList().Where(item => item.ID_Book == selected.ID_Book);
            }
        }
    }
}
