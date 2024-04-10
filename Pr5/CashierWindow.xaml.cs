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
    /// Логика взаимодействия для CashierWindow.xaml
    /// </summary>
    public partial class CashierWindow : Window
    {
        

        public CashierWindow()
        {
            InitializeComponent();
            
        }

        private void purchase_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new CsPurchase();
        }

        private void cheque_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new CsCheque();
        }
    }
}
