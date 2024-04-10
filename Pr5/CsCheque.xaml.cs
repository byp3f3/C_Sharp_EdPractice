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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pr5
{
    /// <summary>
    /// Логика взаимодействия для CsCheque.xaml
    /// </summary>
    public partial class CsCheque : Page
    {
        BookstoreEntities bookstore = new BookstoreEntities();
        public CsCheque()
        {
            InitializeComponent();
            chequeCbx.ItemsSource = bookstore.Purchase.ToList();
            chequeCbx.DisplayMemberPath = "ID_Purchase";
        }

        private void chequeCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (chequeCbx.SelectedItem != null)
            { 
                var selected = chequeCbx.SelectedItem as Purchase;
                List<Book> books = new List<Book>();
                foreach (var item in bookstore.Cheque.ToList().Where(item => item.Purchase_ID == selected.ID_Purchase))
                {
                    foreach(Book book in bookstore.Book.ToList())
                    {
                        if(book.ID_Book == item.Book_ID)
                        {
                            books.Add(book);
                        }
                    }
                }
                goodsDgr.ItemsSource = books.ToList();
                foreach(var item in bookstore.Users.ToList().Where(item => item.ID_User == selected.Users_ID))
                {
                    foreach(Users user in bookstore.Users.ToList())
                    {
                        if(user.ID_User == selected.Users_ID)
                        {
                            employeeTbx.Text = user.Surname + " " + user.FirstName + " " + user.MiddleName;
                        }
                    }
                }
                date.Text = selected.PurchaseDate.ToString();
                sum.Text = selected.Cost.ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Book> books = (List<Book>)goodsDgr.ItemsSource;
            var purchase = chequeCbx.SelectedItem as Purchase;
            string txt = "Книжный\nКассовый чек №" + purchase.ID_Purchase;
            File.WriteAllText("C:\\Users\\HP\\Desktop\\Чек.txt", txt);
            foreach (Book book in books)
            {
                string bookInfo = "\n" + book.Bookname + " - " + book.Cost;
                File.AppendAllText("C:\\Users\\HP\\Desktop\\Чек.txt", bookInfo);
            }
            txt = "\nИтого к оплате: " + purchase.Cost + "\nВнесено: " + purchase.Payment + "\nСдача: " + purchase.PurchaseChange;
            File.AppendAllText("C:\\Users\\HP\\Desktop\\Чек.txt", txt);
        }
    }
}
