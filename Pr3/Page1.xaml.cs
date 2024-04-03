using Pr3.BooksDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class Page1 : Page
    {
        HumanTableAdapter human = new HumanTableAdapter();
        BookTableAdapter book = new BookTableAdapter();
        public Page1()
        {
            InitializeComponent();

            humanDgr.ItemsSource = human.GetData();
            SearchCbx.ItemsSource = book.GetData();
            SearchCbx.DisplayMemberPath = "Book_name";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            humanDgr.ItemsSource = human.SearchByName(SearchTbx.Text);
        }

        private void SearchCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SearchCbx.SelectedItem != null)
            {
                var bookId = (int)(SearchCbx.SelectedItem as DataRowView).Row[0];
                humanDgr.ItemsSource = human.SearchById(bookId);
            }
        }

        private void ClearBut_Click(object sender, RoutedEventArgs e)
        {
            humanDgr.ItemsSource = human.GetData();
            SearchCbx.SelectedItem = null;
            SearchTbx.Clear();
        }

    }
}

