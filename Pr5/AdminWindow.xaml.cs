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
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void role_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new AdRole();
        }

        private void user_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new AdUsers();
        }

        private void logpas_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new AdLogPass();
        }

        private void workplace_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new AdWorkplace();
        }

        private void city_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new AdCity();
        }

        private void type_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new AdType();
        }
    }
}
