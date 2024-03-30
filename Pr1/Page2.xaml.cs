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
//DataSet
//using Pr1.BooksDataSetTableAdapters;

namespace Pr1
{
    /// <summary>
    /// Логика взаимодействия для Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        //EF
        private BooksEntities books = new BooksEntities();
        //DataSet
        //BookTableAdapter book = new BookTableAdapter();
        public Page2()
        {
            InitializeComponent();
            //EF
            booksDgr.ItemsSource = books.Book.ToList();
            //DataSet
            //booksDgr.ItemsSource = book.GetData();
        }
        
    }
}
