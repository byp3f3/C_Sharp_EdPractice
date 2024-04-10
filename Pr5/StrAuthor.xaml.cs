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
using System.Windows.Shapes;

namespace Pr5
{
    /// <summary>
    /// Логика взаимодействия для StrAuthor.xaml
    /// </summary>
    public partial class StrAuthor : Page
    {
        private BookstoreEntities bookstore = new BookstoreEntities();
        public StrAuthor()
        {
            InitializeComponent();
            authorDgr.ItemsSource = bookstore.Author.ToList();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            if(surnameTbx.Text != string.Empty && nameTbx.Text != string.Empty)
            {
                if (Check(surnameTbx.Text) == true && Check(nameTbx.Text) == true && Check(midnameTbx.Text) == true)
                {
                    Author author = new Author();
                    author.Surname = surnameTbx.Text;
                    author.FirstName = nameTbx.Text;
                    author.MiddleName = midnameTbx.Text;
                    bookstore.Author.Add(author);
                    bookstore.SaveChanges();
                    authorDgr.ItemsSource = bookstore.Author.ToList();
                    surnameTbx.Clear();
                    nameTbx.Clear();
                    midnameTbx.Clear();
                }
                else { MessageBox.Show("ФИО может содержать только буквы"); }
            }
            else { MessageBox.Show("Убедитесь, что все поля заполнены"); }
        }

        private void change_Click(object sender, RoutedEventArgs e)
        {
            if(authorDgr.SelectedItem != null && surnameTbx.Text != string.Empty && nameTbx.Text != string.Empty)
            {
                if (Check(surnameTbx.Text) == true && Check(nameTbx.Text) == true && Check(midnameTbx.Text) == true)
                {
                    var author = authorDgr.SelectedItem as Author;
                    author.Surname = surnameTbx.Text;
                    author.FirstName = nameTbx.Text;
                    author.MiddleName = midnameTbx.Text;
                    bookstore.SaveChanges();
                    authorDgr.ItemsSource = bookstore.Author.ToList();
                    surnameTbx.Clear();
                    nameTbx.Clear();
                    midnameTbx.Clear();
                }
                else { MessageBox.Show("ФИО может содержать только буквы"); }
            }
            else { MessageBox.Show("Убедитесь, что все поля заполнены"); }
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            if (authorDgr.SelectedItem != null)
            {
                bookstore.Author.Remove(authorDgr.SelectedItem as Author);
                bookstore.SaveChanges();
                authorDgr.ItemsSource = bookstore.Author.ToList();
                surnameTbx.Clear();
                nameTbx.Clear();
                midnameTbx.Clear();
            }
            else { MessageBox.Show("Убедитесь, что Вы выбрали объект для удаления"); }
        }

        private void authorDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (authorDgr.SelectedItem != null)
            {
                var author = authorDgr.SelectedItem as Author;
                surnameTbx.Text = author.Surname;
                nameTbx.Text = author.FirstName;
                midnameTbx.Text = author.MiddleName;
                author.Surname = surnameTbx.Text;
                author.FirstName = nameTbx.Text;
                author.MiddleName = midnameTbx.Text;
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
