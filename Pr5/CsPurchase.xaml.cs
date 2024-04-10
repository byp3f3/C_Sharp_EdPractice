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
    
    public partial class CsPurchase : Page
    {
        BookstoreEntities bookstore = new BookstoreEntities();
        decimal sum = 0;
        public CsPurchase()
        {
            InitializeComponent();
            goods.ItemsSource = bookstore.Book.ToList();
            sumTbx.Text = "Товары в чеке. Полная цена:" + sum.ToString();
        }
        List<Book> books = new List<Book>();
        
        private void plus_Click(object sender, RoutedEventArgs e)
        {
            if(goods.SelectedItem != null)
            {
                Book selected = goods.SelectedItem as Book;
                selected.Amount -= 1;
                if(selected.Amount >= 0)
                {
                    books.Add(goods.SelectedItem as Book);
                    sum += selected.Cost;
                    sumTbx.Text = "Товары в чеке. Полная цена:" + sum.ToString();
                    purch.ItemsSource = null;
                    purch.ItemsSource = books;
                    goods.ItemsSource = bookstore.Book.ToList();
                }
                else
                {
                    MessageBox.Show("Нет в наличии");
                }
            }
            else
            {
                MessageBox.Show("Выберите товар");
            }
        }

        private void minus_Click(object sender, RoutedEventArgs e)
        {
            if (purch.SelectedItem != null)
            {
                Book selected = purch.SelectedItem as Book;
                selected.Amount += 1;
                books.Remove(purch.SelectedItem as Book);
                sum -= selected.Cost;
                sumTbx.Text = "Товары в чеке. Полная цена:" + sum.ToString();
                purch.ItemsSource = null;
                purch.ItemsSource = books;
                goods.ItemsSource = bookstore.Book.ToList();
                
            }
            else
            {
                MessageBox.Show("Выберите товар в чеке");
            }
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            if (purch.ItemsSource != null && books != null)
            {
                decimal pay;
                if (decimal.TryParse(payment.Text, out pay))
                {
                    if (pay > sum)
                    {
                        Purchase purchase = new Purchase();
                        purchase.PurchaseDate = DateTime.Now;
                        purchase.Cost = sum;
                        purchase.Payment = Convert.ToDecimal(payment.Text);
                        purchase.PurchaseChange = Convert.ToDecimal(payment.Text) - sum;
                        purchase.Users_ID = 3;
                        bookstore.Purchase.Add(purchase);
                        bookstore.SaveChanges();
                        foreach (Book book in books)
                        {
                            Cheque cheque = new Cheque();
                            cheque.Purchase_ID = purchase.ID_Purchase;
                            cheque.Book_ID = book.ID_Book;
                            bookstore.Cheque.Add(cheque);
                            bookstore.SaveChanges();
                        }
                        string txt = "Книжный\nКассовый чек №" + purchase.ID_Purchase;
                        File.WriteAllText("C:\\Users\\HP\\Desktop\\Чек.txt", txt);
                        foreach (Book book in books)
                        {
                            string bookInfo = "\n"+ book.Bookname + " - " + book.Cost;
                            File.AppendAllText("C:\\Users\\HP\\Desktop\\Чек.txt", bookInfo);
                        }
                        txt = "\nИтого к оплате: "+ purchase.Cost + "\nВнесено: " + purchase.Payment + "\nСдача: " + purchase.PurchaseChange;
                        File.AppendAllText("C:\\Users\\HP\\Desktop\\Чек.txt", txt);
                        purch.ItemsSource = null;
                        books = null;
                        sum = 0;
                        sumTbx.Text = "Товары в чеке. Полная цена:" + sum.ToString();
                        payment.Clear();
                    }
                    else MessageBox.Show("Внесено недостаточно средств");
                }
                else MessageBox.Show("Введенное значение не является числом");
            }
            else MessageBox.Show("Для покупки выберите товар");
        }
    }
}
