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
    /// Logika interakcji dla klasy managePackages.xaml
    /// </summary>
    public partial class managePackages : Page
    {
        public managePackages()
        {
            InitializeComponent();
            //show list of all packages
            IMongoCollection<Package> collection = MongoDatabaseSingleton.Instance.database.GetCollection<Package>("Packages");
            packagesList.ItemsSource = collection.Find(_ => true).ToList();
        }

        //method for automatic searching in packages list
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
                    Package p = o as Package;
                    if (t.Name == "txtName")
                        return (p.Name.ToUpper().StartsWith(filter.ToUpper()));
                    if (t.Name == "txtPrice")
                        return (p.Price == Convert.ToInt32(filter));
                    if (t.Name == "txtTimeLimit")
                        return (p.TimeLimit == Convert.ToInt32(filter));
                    return (p.EntrancesLimit == Convert.ToInt32(filter));
                };
            }
        }

        private void DataGridCell_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //get package that has been clicked
            DependencyObject dep = (DependencyObject)e.OriginalSource;

            while (!(dep is DataGridCell))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            DataGridCell cell = dep as DataGridCell;
            Package p = (Package)cell.DataContext;

            //show managing customer panel
            MainWindow.MainFrame.Content = new managePackage(p);
        }

        //clicking enter
        private void DataGridCell_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //get package that has been clicked
                DependencyObject dep = (DependencyObject)e.OriginalSource;

                while (!(dep is DataGridCell))
                {
                    dep = VisualTreeHelper.GetParent(dep);
                }

                DataGridCell cell = dep as DataGridCell;
                Package p = (Package)cell.DataContext;

                //show managing customer panel
                MainWindow.MainFrame.Content = new managePackage(p);
            }
        }

        private void newPackage(object sender, RoutedEventArgs e)
        {
            //show page for adding new package
            MainWindow.MainFrame.Content = new AddPackage();

        }
    }
}
