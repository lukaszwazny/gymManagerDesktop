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
    /// Logika interakcji dla klasy packageHistory.xaml
    /// </summary>
    public partial class packageHistory : Page
    {
        //customer whose packages are being shown
        private Customer customer;
        public packageHistory(Customer c)
        {
            //remember customer
            customer = c;
            InitializeComponent();
            text.Text = "Karnety klienta " + c.Name + " " + c.Surname;
            //get bought packages collection
            IMongoCollection<BoughtPackage> collection = MongoDatabaseSingleton.Instance.database.GetCollection<BoughtPackage>("BoughtPackages");
            //find bought packages bought by customer
            List<BoughtPackage> customersPackages = collection.Find(bp => bp.CustomerId == c.Id).ToList();
            //get list of package objects that customer bought
            List<Package> packages = new List<Package>();
            IMongoCollection<Package> packagesCollection = MongoDatabaseSingleton.Instance.database.GetCollection<Package>("Packages");
            customersPackages.ForEach(cp =>
            {
                List<Package> p = packagesCollection.Find(pac => pac.Id == cp.PackageId).Limit(1).ToList();
                packages.Add(p.ElementAt(0));
            });
            //make list of boughtPackagesToShow objects for displaying data abiut bought packages
            List<boughtPackagesToShow> bptss = new List<boughtPackagesToShow>();
            customersPackages.ForEach(cp =>
            {
                Package p = packages.ElementAt(customersPackages.IndexOf(cp));
                boughtPackagesToShow bpts = new boughtPackagesToShow
                {
                    Name = p.Name,
                    Price = p.Price,
                    TimeLimit = p.TimeLimit,
                    EntrancesLimit = p.EntrancesLimit,
                    PurchaseDate = cp.PurchaseDate
                };
                bptss.Add(bpts);
            });
            //display
            packagesList.ItemsSource = bptss;
        }

        private void buyPackage(object sender, RoutedEventArgs e)
        {
            //show buy package page
            MainWindow.MainFrame.Content = new BuyPackage(customer);
        }

        //method for automatic searching 
        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            string filter = t.Text;
            ICollectionView cv = CollectionViewSource.GetDefaultView(packagesList.ItemsSource);
            if (filter == "")
                cv.Filter = null;
            else
            {
                cv.Filter = o =>
                {
                    boughtPackagesToShow p = o as boughtPackagesToShow;
                    if (t.Name == "txtName")
                        return (p.Name.ToUpper().StartsWith(filter.ToUpper()));
                    if (t.Name == "txtPrice")
                        return (p.Price == Convert.ToInt32(filter));
                    if (t.Name == "txtTimeLimit")
                        return (p.TimeLimit == Convert.ToInt32(filter));
                    if (t.Name == "txtPurchaseDate")
                        return (p.PurchaseDate.ToString().ToUpper().StartsWith(filter.ToUpper()));
                    return (p.EntrancesLimit == Convert.ToInt32(filter));
                };
            }
        }
    }

    //class just for showing data about bought packages
    public class boughtPackagesToShow
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int TimeLimit { get; set; }       //unit - day; 0 means unlimited
        public int EntrancesLimit { get; set; }  //0 means unlimited
        public DateTime PurchaseDate { get; set; }
    }
}
