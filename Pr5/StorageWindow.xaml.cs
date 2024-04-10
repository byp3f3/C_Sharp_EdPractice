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
    /// Логика взаимодействия для StorageWindow.xaml
    /// </summary>
    public partial class StorageWindow : Window
    {
        public StorageWindow()
        {
            InitializeComponent();
        }

        private void book_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new StrBook();
        }

        private void author_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new StrAuthor();
        }

        private void pubhouse_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new StrPubHouse();
        }
    }
}
