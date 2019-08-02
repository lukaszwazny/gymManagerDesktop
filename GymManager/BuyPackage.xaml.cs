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
    /// Logika interakcji dla klasy BuyPackage.xaml
    /// </summary>
    public partial class BuyPackage : Page
    {
        private Customer customer;
        private IMongoCollection<Package> packagesCollection;
        public BuyPackage(Customer c)
        {
            InitializeComponent();
            title.Text = "Kup karnet dla " + c.Name + " " + c.Surname;
            //get packages collection
            packagesCollection = MongoDatabaseSingleton.Instance.database.GetCollection<Package>("Packages");
            //get list of all packages
            List<Package> packages = packagesCollection.Find(_ => true).ToList();
            //get list of all packages names
            List<string> packagesNames = new List<string>();
            packages.ForEach(p =>
            {
                packagesNames.Add(p.Name);
            });
            //show packages names
            packagesList.ItemsSource = packagesNames;
            //make selected date default - now
            date.SelectedDate = DateTime.Now;
            //remember customer (for buy method)
            customer = c;
        }

        private void buy(object sender, RoutedEventArgs e)
        {
            try
            {
                //required field
                if (packagesList.Text == "")
                    throw new Exception("Nazwa nie może być pusta!");

                //get bought packages collection
                IMongoCollection<BoughtPackage> collection = MongoDatabaseSingleton.Instance.database.GetCollection<BoughtPackage>("BoughtPackages");

                //get list of one package with name selected by user
                List<Package> packages = packagesCollection.Find(p => p.Name == packagesList.Text).ToList();

                //create new bought package
                BoughtPackage c = new BoughtPackage
                {
                    CustomerId = customer.Id,
                    PackageId = packages.ElementAt(0).Id,
                    PurchaseDate = date.SelectedDate.Value
                };

                //add to collection
                collection.InsertOne(c);

                //show managing customer page
                MainWindow.MainFrame.Content = new manageCustomer(customer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
