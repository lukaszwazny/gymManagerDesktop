﻿using MongoDB.Driver;
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
    /// Logika interakcji dla klasy managePackage.xaml
    /// </summary>
    public partial class managePackage : Page
    {
        //package that is being edited
        private Package package;
        public managePackage(Package p)
        {
            InitializeComponent();
            //fill all fields with customer data
            Name.Text = p.Name;
            Price.Text = Convert.ToString(p.Price);
            Description.Document.Blocks.Clear();
            Description.Document.Blocks.Add(new Paragraph(new Run(p.Description)));
            TimeLimit.Text = Convert.ToString(p.TimeLimit);
            EntrancesLimit.Text = Convert.ToString(p.EntrancesLimit);
            package = p;
            
        }

        private void updatePackage(object sender, RoutedEventArgs e)
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

                //defining what to update and with what data
                UpdateDefinition<Package> update = Builders<Package>.Update
                .Set(c => c.Name, Name.Text)
                .Set(c => c.Price, Convert.ToDouble(Price.Text))
                .Set(c => c.Description, new TextRange(Description.Document.ContentStart, Description.Document.ContentEnd).Text)
                .Set(c => c.TimeLimit, Convert.ToInt32(TimeLimit.Text))
                .Set(c => c.EntrancesLimit, Convert.ToInt32(EntrancesLimit.Text));

                //get packages collection
                IMongoCollection<Package> collection = MongoDatabaseSingleton.Instance.database.GetCollection<Package>("Packages");

                //update package
                collection.FindOneAndUpdate(c => c.Id == package.Id, update);

                //show packages list
                MainWindow.MainFrame.Content = new managePackages();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void deletePackage(object sender, RoutedEventArgs e)
        {
            //get packages collection
            IMongoCollection<Package> collection = MongoDatabaseSingleton.Instance.database.GetCollection<Package>("Packages");

            //message box for safety reasons
            MessageBoxResult areYouSure = MessageBox.Show("Czy na pewno chcesz usunąć ten karnet?", "Uwaga!", MessageBoxButton.YesNo);

            if (areYouSure == MessageBoxResult.Yes)
            {
                //deleting
                collection.FindOneAndDelete(c => c.Id == package.Id);
                MainWindow.MainFrame.Content = new managePackages();
            }

        }
    }
}
