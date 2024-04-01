using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
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
        private BooksEntities books = new BooksEntities();
        //DataSet
        //HumanTableAdapter human = new HumanTableAdapter();
        public Page1()
        {
            InitializeComponent();
            //EF
            humanDgr.ItemsSource = books.Human.ToList();
            bookCbx.ItemsSource = books.Book.ToList();
            bookCbx.DisplayMemberPath = "Book_name";
            bookCbx.SelectedValuePath = "ID_Book";
            //DataSet
           // humanDgr.ItemsSource = human.GetData();
        }

        //добавление
        private void add_Click(object sender, RoutedEventArgs e)
        {
            Human human = new Human();
            human.Surname = surname.Text;
            human.FirstName = name.Text;
            human.Middle_name = midname.Text;
            human.Book_ID = (int)bookCbx.SelectedValue;
            books.Human.Add(human);
            books.SaveChanges();
            humanDgr.ItemsSource = books.Human.ToList();
            name.Clear();
            surname.Clear();
            midname.Clear();
        }

        //изменение
        private void change_Click(object sender, RoutedEventArgs e)
        {
            if (humanDgr.SelectedItem != null)
            {
                var selected = (Human)humanDgr.SelectedItem;
                selected.Surname = surname.Text;
                selected.FirstName = name.Text;
                selected.Middle_name= midname.Text;
                selected.Book_ID = (int)bookCbx.SelectedValue;
                books.SaveChanges();
                humanDgr.ItemsSource = books.Human.ToList();
                name.Clear();
                surname.Clear();
                midname.Clear();
            }
        }

        //удаление
        private void del_Click(object sender, RoutedEventArgs e)
        {
            if( humanDgr.SelectedItem != null )
            {
                books.Human.Remove(humanDgr.SelectedItem as Human); 
                books.SaveChanges();
                humanDgr.ItemsSource = books.Human.ToList();
                name.Clear();
                surname.Clear();
                midname.Clear();
            }
            
        }

        private void humanDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (Human)humanDgr.SelectedItem;
            if (selected != null)
            {
                surname.Text = selected.Surname;
                name.Text = selected.FirstName;
                midname.Text = selected.Middle_name;
                bookCbx.SelectedValue = selected.Book_ID;
                selected.Surname = surname.Text;
                selected.FirstName = name.Text;
                selected.Middle_name = midname.Text;
                selected.Book_ID = (int)bookCbx.SelectedValue;
            }
        }
    }
}
