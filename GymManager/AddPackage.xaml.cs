using MongoDB.Driver;
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
    /// Logika interakcji dla klasy AddPackage.xaml
    /// </summary>
    public partial class AddPackage : Page
    {
        public AddPackage()
        {
            InitializeComponent();
        }

        private void newPackage(object sender, RoutedEventArgs e)
        {
            try
            {
                //required fields
                if (Name.Text == "")
                    throw new Exception("Nazwa nie może być pusta!");
                if (Price.Text == "")
                    throw new Exception("Cena nie może być pusta!");
                if (TimeLimit.Text == "")
                    throw new Exception("Czas trwania nie może być pusty!");
                if (EntrancesLimit.Text == "")
                    throw new Exception("Limit wejść nie może być pusty!");

                //create new package
                Package c = new Package
                {
                    Name = Name.Text,
                    Price = Convert.ToDouble(Price.Text),
                    Description = new TextRange(Description.Document.ContentStart, Description.Document.ContentEnd).Text,
                    TimeLimit = Convert.ToInt32(TimeLimit.Text),
                    EntrancesLimit = Convert.ToInt32(EntrancesLimit.Text),
                    ForFamily = (bool)forFamily.IsChecked
                };

                //add package
                c.add();

                //show packages
                MainWindow.MainFrame.Content = new managePackages();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
