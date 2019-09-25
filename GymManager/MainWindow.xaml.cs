using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using static MongoDB.Bson.Serialization.BsonSerializationContext;

namespace GymManager
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //frame wich will be filled with different pages
        public static Frame MainFrame;

        public MainWindow()
        {
            InitializeComponent();

            MainFrame = frame;
        }

        //handling all three menu items
        private void customersShow(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Customers();
        }

        private void entrances(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new showEntrances();
        }

        private void configuration(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new configuration();
        }
    }
}
