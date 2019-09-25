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

namespace GymManager
{
    /// <summary>
    /// Logika interakcji dla klasy configuration.xaml
    /// </summary>
    public partial class configuration : Page
    {
        public configuration()
        {
            InitializeComponent();
        }

        private void managePackages(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrame.Content = new managePackages();
        }

        private void showPaymentTypes(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrame.Content = new showPaymentTypes();
        }
    }
}
