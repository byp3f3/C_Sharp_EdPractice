using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;

namespace Pr5
{
    /// <summary>
    /// Логика взаимодействия для StrBook.xaml
    /// </summary>
    public partial class StrBook : Page
    {
        private BookstoreEntities bookstore = new BookstoreEntities();
        public StrBook()
        {
            InitializeComponent();
            bookDgr.ItemsSource = bookstore.Book.ToList();
            authorCbx.ItemsSource = bookstore.Author.ToList();
            pubHouseCbx.ItemsSource = bookstore.PublishingHouse.ToList();
            authorCbx.DisplayMemberPath = "Surname";
            authorCbx.SelectedValuePath = "ID_Author";
            pubHouseCbx.DisplayMemberPath = "PHName";
            pubHouseCbx.SelectedValuePath = "ID_PubHouse";
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            if(nameTbx.Text != string.Empty && amountTbx.Text != string.Empty && costTbx.Text != string.Empty && authorCbx.SelectedItem != null && pubHouseCbx.SelectedItem != null)
            {
                int amount;
                if (int.TryParse(amountTbx.Text, out amount) && amount > 0)
                {
                    decimal cost;
                    if (decimal.TryParse(costTbx.Text, out cost) && cost > 0)
                    {
                        Book book = new Book();
                        book.Bookname = nameTbx.Text;
                        book.Author_ID = (int)authorCbx.SelectedValue;
                        book.PubHouse_ID = (int)(pubHouseCbx.SelectedValue);
                        book.Amount = amount;
                        book.Cost = Convert.ToDecimal(costTbx.Text);
                        bookstore.Book.Add(book);
                        bookstore.SaveChanges();
                        bookDgr.ItemsSource = bookstore.Book.ToList();
                        nameTbx.Clear();
                        amountTbx.Clear();
                        costTbx.Clear();
                        authorCbx.SelectedItem = null;
                        pubHouseCbx.SelectedItem = null;
                    }
                    else MessageBox.Show("Стоимость должна быть положительным числом");
                }
                else MessageBox.Show("Количество должно быть положительным числом");
            }
            else { MessageBox.Show("Убедитесь, что все поля заполнены"); }
        }

        private void change_Click(object sender, RoutedEventArgs e)
        {
            if(bookDgr.SelectedItem != null && nameTbx.Text != string.Empty && amountTbx.Text != string.Empty && costTbx.Text != string.Empty && authorCbx.SelectedItem != null && pubHouseCbx.SelectedItem != null)
            {
                int amount;
                if (int.TryParse(amountTbx.Text, out amount) && amount > 0)
                {
                    decimal cost;
                    if (decimal.TryParse(costTbx.Text, out cost) && cost > 0)
                    {
                        var book = bookDgr.SelectedItem as Book;
                        book.Bookname = nameTbx.Text;
                        book.Author_ID = (int)authorCbx.SelectedValue;
                        book.PubHouse_ID = (int)(pubHouseCbx.SelectedValue);
                        book.Amount = Convert.ToInt32(amountTbx.Text);
                        book.Cost = Convert.ToDecimal(costTbx.Text);
                        bookstore.SaveChanges();
                        bookDgr.ItemsSource = bookstore.Book.ToList();
                        nameTbx.Clear();
                        amountTbx.Clear();
                        costTbx.Clear();
                        authorCbx.SelectedItem = null;
                        pubHouseCbx.SelectedItem = null;
                    }
                    else MessageBox.Show("Стоимость должна быть положительным числом");
                }
                else MessageBox.Show("Количество должно быть положительным числом");
            }
            else { MessageBox.Show("Убедитесь, что все поля заполнены"); }
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            if (bookDgr.SelectedItem != null)
            {
                bookstore.Book.Remove(bookDgr.SelectedItem as Book);
                bookstore.SaveChanges();
                bookDgr.ItemsSource = bookstore.Book.ToList();
                nameTbx.Clear();
                amountTbx.Clear();
                costTbx.Clear();
                authorCbx.SelectedItem = null;
                pubHouseCbx.SelectedItem = null;
            }
            else { MessageBox.Show("Убедитесь, что Вы выбрали объект для удаления"); }
        }

        private void bookDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (bookDgr.SelectedItem != null)
            {
                var book = bookDgr.SelectedItem as Book;
                nameTbx.Text = book.Bookname;
                amountTbx.Text = book.Amount.ToString();
                costTbx.Text = book.Cost.ToString();
                authorCbx.SelectedValue = book.Author_ID;
                pubHouseCbx.SelectedValue = book.PubHouse_ID;
                book.Bookname = nameTbx.Text;
                book.Author_ID = (int)authorCbx.SelectedValue;
                book.PubHouse_ID = (int)(pubHouseCbx.SelectedValue);
                book.Amount = Convert.ToInt32(amountTbx.Text);
                book.Cost = Convert.ToDecimal(costTbx.Text);
            }
        }

        private void search_click(object sender, RoutedEventArgs e)
        {
            bookDgr.ItemsSource = bookstore.Book.ToList().Where(item => item.Bookname.Contains(search.Text));
        }

        private void clear_click(object sender, RoutedEventArgs e)
        {
            bookDgr.ItemsSource = bookstore.Book.ToList();
            search.Clear();
        }

        public static T DeserializeObject<T>()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string json = File.ReadAllText(dialog.FileName);
                T obj = JsonConvert.DeserializeObject<T>(json);
                return obj;
            }
            else
            {
                return default(T);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Book> books = DeserializeObject<List<Book>>();
            foreach(Book book in books)
            {
                bookstore.Book.Add(book);
                bookstore.SaveChanges();
                bookDgr.ItemsSource = bookstore.Book.ToList();
            }
        }
    }
}
