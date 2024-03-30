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
using Pr1.BooksDataSetTableAdapters;

namespace Pr1
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {       
        //EF
        //private BooksEntities books = new BooksEntities();
        //DataSet
        HumanTableAdapter human = new HumanTableAdapter();
        public Page1()
        {
            InitializeComponent();
            //EF
            //humanDgr.ItemsSource = books.Human.ToList();
            //DataSet
            humanDgr.ItemsSource = human.GetData();
        }
    }
}
