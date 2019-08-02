using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Logika interakcji dla klasy showEntrances.xaml
    /// </summary>
    public partial class showEntrances : Page
    {
        public showEntrances()
        {
            InitializeComponent();
            //get entrances collection
            IMongoCollection<Entrance> collection = MongoDatabaseSingleton.Instance.database.GetCollection<Entrance>("Entrances");

            //get all entrances to list
            List<Entrance> entrances = collection.Find(_ => true).ToList();

            //get entrances collection
            IMongoCollection<Customer> customersCollection = MongoDatabaseSingleton.Instance.database.GetCollection<Customer>("Customers");

            //get list of customers that are in each entrance
            List<Customer> customers = new List<Customer>();
            entrances.ForEach(e => {
                List<Customer> c = customersCollection.Find(cus => cus.Id == e.CustomerId).ToList();
                customers.Add(c.ElementAt(0));
            });

            //get bought packages collection
            IMongoCollection<BoughtPackage> boughtPackagesCollection = MongoDatabaseSingleton.Instance.database.GetCollection<BoughtPackage>("BoughtPackages");

            //get list of bought packages that are in each entrance
            List<BoughtPackage> boughtPackages = new List<BoughtPackage>();
            entrances.ForEach(e => {
                List<BoughtPackage> bp = boughtPackagesCollection.Find(pac => pac.Id == e.BoughtPackageId).ToList();
                boughtPackages.Add(bp.ElementAt(0));
            });

            //get packages collection
            IMongoCollection<Package> packagesCollection = MongoDatabaseSingleton.Instance.database.GetCollection<Package>("Packages");

            //get list of packages that are in each entrance
            List<Package> packages = new List<Package>();
            boughtPackages.ForEach(bp => {
                List<Package> p = packagesCollection.Find(pac => pac.Id == bp.PackageId).ToList();
                packages.Add(p.ElementAt(0));
            });

            //make list of EntrancesToShow objects for displaying data about entrances
            List<EntrancesToShow> etss = new List<EntrancesToShow>();
            customers.ForEach(c =>
            {
                Package p = packages.ElementAt(customers.IndexOf(c));
                Entrance e = entrances.ElementAt(customers.IndexOf(c));
                EntrancesToShow ets = new EntrancesToShow
                {
                    Name = c.Name,
                    Surname = c.Surname,
                    packageName = p.Name,
                    entranceDate = e.Date
                };
                etss.Add(ets);
            });
            //display
            entrancesList.ItemsSource = etss;
        }

        //method for automatic searching
        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            string filter = t.Text;
            ICollectionView cv = CollectionViewSource.GetDefaultView(entrancesList.ItemsSource);
            if (filter == "")
                cv.Filter = null;
            else
            {
                cv.Filter = o =>
                {
                    EntrancesToShow p = o as EntrancesToShow;
                    if (t.Name == "txtName")
                        return (p.Name.ToUpper().StartsWith(filter.ToUpper()));
                    if (t.Name == "txtSurname")
                        return (p.Surname.ToUpper().StartsWith(filter.ToUpper()));
                    if (t.Name == "txtSurname")
                        return (p.packageName.ToUpper().StartsWith(filter.ToUpper()));
                    return (p.entranceDate.ToString().ToUpper().StartsWith(filter.ToUpper()));
                };
            }
        }
    }

    //class just for showing data about bought packages
    public class EntrancesToShow
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string packageName { get; set; }      
        public DateTime entranceDate { get; set; }
    }
}
