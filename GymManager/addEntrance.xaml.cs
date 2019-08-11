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
    /// Logika interakcji dla klasy addEntrance.xaml
    /// </summary>
    public partial class addEntrance : Page
    { 

        //Customer for which we are adding entrance
        private Customer customer;  

        public addEntrance(Customer c)
        {
            InitializeComponent();
            title.Text = "Zarejestruj wejście " + c.Name + " " + c.Surname;
            //get packages bought by customer in list of BoughtPackage objects
            List<BoughtPackage> customersPackages = c.getBoughtPackages();
            //get list of package names bought by customer
            List<string> packagesNames = new List<string>();
            customersPackages.ForEach(p =>
            {
                packagesNames.Add(p.getName());
            });
            //display these names
            packagesList.ItemsSource = packagesNames;
            //make selected default date - now
            date.SelectedDate = DateTime.Now;
            //remember customer (for add method)
            customer = c;
        }

        private void add(object sender, RoutedEventArgs e)
        {
            try
            {
                //name of package required
                if (packagesList.Text == "")
                    throw new Exception("Nazwa nie może być pusta!");

                //get bought package selected by user
                BoughtPackage boughtPackage = customer.getBoughtPackageByName(packagesList.Text);

                //create new entrance
                Entrance c = new Entrance
                {
                    CustomerId = customer.Id,
                    BoughtPackageId = boughtPackage.Id,
                    Date = date.SelectedDate.Value
                };
                

                //add entrance
                c.add();

                //show entrances
                MainWindow.MainFrame.Content = new showEntrances();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
