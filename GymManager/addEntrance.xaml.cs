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

        private List<BoughtPackage> boughtPackages;

        public addEntrance(Customer c)
        {
            InitializeComponent();
            title.Text = "Zarejestruj wejście " + c.Name + " " + c.Surname;
            //get bought packages collection
            IMongoCollection<BoughtPackage> collection = MongoDatabaseSingleton.Instance.database.GetCollection<BoughtPackage>("BoughtPackages");
            //get packages bought by customer in list of BoughtPackage objects
            List<BoughtPackage> customersPackages = collection.Find(bp => bp.CustomerId == c.Id).ToList();
            //get packages bought by customer in list of Package objects
            List<Package> packages = new List<Package>();
            IMongoCollection<Package> packagesCollection = MongoDatabaseSingleton.Instance.database.GetCollection<Package>("Packages");
            customersPackages.ForEach(cp =>
            {
                List<Package> p = packagesCollection.Find(pac => pac.Id == cp.PackageId).Limit(1).ToList();
                packages.Add(p.ElementAt(0));
            });
            //get list of package names bought by customer
            List<string> packagesNames = new List<string>();
            packages.ForEach(p =>
            {
                packagesNames.Add(p.Name);
            });
            //display these names
            packagesList.ItemsSource = packagesNames;
            //make selected default date - now
            date.SelectedDate = DateTime.Now;
            //remember customer (for add method)
            customer = c;
            //remember bought packages (for add method)
            boughtPackages = customersPackages;
        }

        private void add(object sender, RoutedEventArgs e)
        {
            try
            {
                //name of package required
                if (packagesList.Text == "")
                    throw new Exception("Nazwa nie może być pusta!");

                //get entrances collection
                IMongoCollection<Entrance> collection = MongoDatabaseSingleton.Instance.database.GetCollection<Entrance>("Entrances");

                //get list of one package with name selected by user
                IMongoCollection<Package> packagesCollection = MongoDatabaseSingleton.Instance.database.GetCollection<Package>("Packages");
                List<Package> packages = packagesCollection.Find(p => p.Name == packagesList.Text).ToList();

                //get bought package selected by user
                BoughtPackage boughtPackage = new BoughtPackage();
                boughtPackages.ForEach(bp =>
                {
                    if (bp.PackageId == packages.ElementAt(0).Id)
                        boughtPackage = bp;
                });

                //create new entrance
                Entrance c = new Entrance
                {
                    CustomerId = customer.Id,
                    BoughtPackageId = boughtPackage.Id,
                    Date = date.SelectedDate.Value
                };

                //add entrance
                collection.InsertOne(c);

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
