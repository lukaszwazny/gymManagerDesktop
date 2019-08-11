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
            
            //display
            entrancesList.ItemsSource = Entrance.getEntrancesToShow().OrderByDescending(x => x.entranceDate);
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

        //method for automatic searching - different way
        private void txtNameTextChanged(object sender, TextChangedEventArgs e)
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
                    if (whatToSearch.Text == "Imię")
                        return (p.Name.ToUpper().StartsWith(filter.ToUpper()));
                    if (whatToSearch.Text == "Nazwisko")
                        return (p.Surname.ToUpper().StartsWith(filter.ToUpper()));
                    if (whatToSearch.Text == "Nazwa karnetu")
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
