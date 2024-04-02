using Pr3.BooksDataSetTableAdapters;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //EF
        //private BooksEntities books = new BooksEntities();

        //DataSet
        HumanTableAdapter human = new HumanTableAdapter();
        public MainWindow()
        {
            InitializeComponent();
            //EF
            //humanDgr.ItemsSource = books.Human.ToList();

            //DataSet
            humanDgr.ItemsSource = human.GetDataBy();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            humanDgr.Columns[0].Visibility = Visibility.Hidden;
            humanDgr.Columns[4].Visibility = Visibility.Hidden;

        }
    }
}
