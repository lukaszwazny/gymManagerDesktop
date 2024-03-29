﻿using MongoDB.Driver;
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
using System.Xml.Linq;

namespace GymManager
{
    /// <summary>
    /// Logika interakcji dla klasy Customers.xaml
    /// </summary>
    public partial class Customers : Page
    {
        public Customers()
        {
            InitializeComponent();

            //show all customers
            IMongoCollection<Customer> collection = MongoDatabaseSingleton.Instance.database.GetCollection<Customer>("Customers");
            customersList.ItemsSource = collection.Find(_ => true).ToList();
        }

        //method for automatic searching in customers list
        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            string filter = t.Text;
            ICollectionView cv = CollectionViewSource.GetDefaultView(customersList.ItemsSource);
            if (filter == "")
                cv.Filter = null;
            else
            {
                cv.Filter = o =>
                {
                    Customer p = o as Customer;
                    if (t.Name == "txtId")
                        return (p.Id == Convert.ToInt32(filter));
                    if (t.Name == "txtName")
                        return (p.Name.ToUpper().StartsWith(filter.ToUpper()));
                    if (t.Name == "txtSurname")
                        return (p.Surname.ToUpper().StartsWith(filter.ToUpper()));
                    if (t.Name == "txtPhone")
                        return (p.Phone.ToUpper().StartsWith(filter.ToUpper()));
                    if (t.Name == "txtBirthday")
                        return (p.Birthday.ToString().ToUpper().StartsWith(filter.ToUpper()));
                    if (t.Name == "txtJoinDate")
                        return (p.JoinDate.ToString().ToUpper().StartsWith(filter.ToUpper()));
                    return (p.CardNumber.ToUpper().StartsWith(filter.ToUpper()));
                };
            }
        }

        //method for automatic searching in customers list - different way
        private void txtNameTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            string filter = t.Text;
            ICollectionView cv = CollectionViewSource.GetDefaultView(customersList.ItemsSource);
            if (filter == "")
                cv.Filter = null;
            else
            {
                cv.Filter = o =>
                {
                    Customer p = o as Customer;
                    if (whatToSearch.Text == "ID")
                        return (p.Id == Convert.ToInt32(filter));
                    if (whatToSearch.Text == "Imię")
                        return (p.Name.ToUpper().StartsWith(filter.ToUpper()));
                    if (whatToSearch.Text == "Nazwisko")
                        return (p.Surname.ToUpper().StartsWith(filter.ToUpper()));
                    if (whatToSearch.Text == "Nr tel")
                        return (p.Phone.ToUpper().StartsWith(filter.ToUpper()));
                    if (whatToSearch.Text == "Data ur.")
                        return (p.Birthday.ToString().ToUpper().StartsWith(filter.ToUpper()));
                    if (whatToSearch.Text == "Data doł. do klubu")
                        return (p.JoinDate.ToString().ToUpper().StartsWith(filter.ToUpper()));
                    return (p.CardNumber.ToUpper().StartsWith(filter.ToUpper()));
                };
            }
        }

        private void newCustomer(object sender, RoutedEventArgs e)
        {
            //show page for adding new customer
            MainWindow.MainFrame.Content = new AddCustomer();
        }

        //manage customer
        private void DataGridCell_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //get customer that has been clicked
            DependencyObject dep = (DependencyObject)e.OriginalSource;

            while (!(dep is DataGridCell))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            DataGridCell cell = dep as DataGridCell;
            Customer c = (Customer)cell.DataContext;

            //show managing customer panel
            MainWindow.MainFrame.Content = new manageCustomer(c);
        }

        //clicking enter
        private void DataGridCell_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //get customer that has been clicked
                DependencyObject dep = (DependencyObject)e.OriginalSource;

                while (!(dep is DataGridCell))
                {
                    dep = VisualTreeHelper.GetParent(dep);
                }

                DataGridCell cell = dep as DataGridCell;
                Customer c = (Customer)cell.DataContext;

                //show managing customer panel
                MainWindow.MainFrame.Content = new manageCustomer(c);
            }
        }
    }
}
